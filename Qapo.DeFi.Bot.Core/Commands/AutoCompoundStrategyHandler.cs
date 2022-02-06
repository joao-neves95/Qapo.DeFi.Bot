using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Numerics;

using MediatR;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;

using Qapo.DeFi.Bot.Core.Interfaces.Stores;
using Qapo.DeFi.Bot.Core.Interfaces.Services;
using Qapo.DeFi.Bot.Core.Interfaces.Web3Services;
using Qapo.DeFi.Bot.Core.Interfaces.Web3Services.External;
using Qapo.DeFi.Bot.Core.Models.Data;
using Qapo.DeFi.Bot.Core.Models.Web3.External;
using Qapo.DeFi.Bot.Core.Models.Web3.LockedStratModels;
using Qapo.DeFi.Bot.Core.Factories;
using Qapo.DeFi.Bot.Core.Extensions;

namespace Qapo.DeFi.Bot.Core.Commands
{
    public class AutoCompoundStrategyHandler : IRequestHandler<AutoCompoundStrategy, bool>
    {
        private readonly ILockedVaultsStore _lockedVaultsStore;

        private readonly IBlockchainStore _blockchainStore;

        private readonly IDexStore _dexStore;

        private readonly ITokenStore _tokenStore;

        private readonly IGasPriceService _gasPriceService;

        private readonly ILoggerService _logger;

        public AutoCompoundStrategyHandler(
            ILockedVaultsStore lockedVaultsStore,
            IBlockchainStore blockchainStore,
            IDexStore dexStore,
            ITokenStore tokenStore,
            IGasPriceService gasPriceService,
            ILoggerService logger
        )
        {
            this._lockedVaultsStore = lockedVaultsStore.ThrowIfNull(nameof(ILockedVaultsStore));
            this._blockchainStore = blockchainStore.ThrowIfNull(nameof(IBlockchainStore));
            this._dexStore = dexStore.ThrowIfNull(nameof(IDexStore));
            this._tokenStore = tokenStore.ThrowIfNull(nameof(ITokenStore));
            this._gasPriceService = gasPriceService.ThrowIfNull(nameof(ILoggerService));
            this._logger = logger.ThrowIfNull(nameof(ILoggerService));
        }

        public async Task<bool> Handle(AutoCompoundStrategy request, CancellationToken cancellationToken)
        {
            this._logger.LogInformation($"Running {nameof(AutoCompoundStrategyHandler)} for {request.LockedVault.Name}...");

            request.AppConfig.ThrowIfNull(nameof(request.AppConfig));

            if (request.LockedVault.LastFarmedTimestamp == null)
            {
                request.LockedVault.LastFarmedTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                await this._lockedVaultsStore.Update(request.LockedVault);

                this._logger.LogInformation("Canceled (first run).");

                return false;
            }

            Blockchain currentBlockchain = await this._blockchainStore.GetByChainId(request.LockedVault.BlockchainId);

            Web3 web3 = new Web3(
                new Account(request.AppConfig.SecretsConfig.WalletPrivateKey, request.LockedVault.BlockchainId),
                currentBlockchain.RpcUrl
            );

            if (await this.IsToCancelExecution(request, web3))
            {
                return false;
            }

            ILockedStratService currentStratServiceHandler = LockedStratServicesFactory.Get(
                LockedStratServiceType.CommonLockedStratService,
                web3,
                request.LockedVault.VaultAddress
            );

            this._logger.LogInformation("Calculating if pending reward amount is profitable for strategy execution.");

            BigInteger pendingRewardAmount = await currentStratServiceHandler.GetPendingRewardAmountQueryAsync();
            // BigInteger pendingRewardAmount = 1314855264749854181;
            // BigInteger pendingRewardAmount = 131481;

            this._logger.LogInformation($"Pending reward amount in decimal: {Web3.Convert.FromWei(pendingRewardAmount)}");

            Dex dex = (await this._dexStore.GetById(request.LockedVault.DexId)).ThrowIfNull("_dexStore.GetById");

            IUniswapV2RouterService uniswapV2RouterServiceHandler = UniswapV2RouterServicesFactory.Get(
                dex.UniswapV2RouterServiceType,
                web3,
                dex.UniswapV2RouterAddress
            );

            BigInteger pendingRewardValueInGas = (await uniswapV2RouterServiceHandler.GetAmountsOutQueryAsync(
                new GetAmountsOutFunction()
                {
                    AmountIn = pendingRewardAmount,
                    Path = new List<string>()
                    {
                        request.LockedVault.RewardAssetAddress,
                        await this._tokenStore.GetAddressById(currentBlockchain.NativeTokenId)
                    }
                }
            ))[1];

            this._logger.LogInformation($"Pending reward value in gas (native token): {pendingRewardValueInGas}");
            this._logger.LogInformation($"> Pending reward value in decimal gas (native token): {Web3.Convert.FromWei(pendingRewardValueInGas)}");

            this._logger.LogInformation($"Calculating transaction fee for .Execute()");

            BigInteger executionGasEstimate = (await currentStratServiceHandler.ContractHandler.EstimateGasAsync<ExecuteFunction>()).Value;
            // BigInteger executionGasEstimate = 164396;

            float gasPrice = await this._gasPriceService.GetStandardGasPrice(request.LockedVault.BlockchainId);

            BigInteger totalTxFee = Web3.Convert.ToWei(
                executionGasEstimate * (int)Math.Round((double)gasPrice, MidpointRounding.ToEven),
                UnitConversion.EthUnit.Gwei
            );

            this._logger.LogInformation($"Execution gas price (gwey): {gasPrice}");
            this._logger.LogInformation($"Execution gas limit (units): {executionGasEstimate}");
            this._logger.LogInformation($"> Total transaction fee in decimal: {Web3.Convert.FromWei(totalTxFee)}");

            bool isMaxSecondsBetweenExecution =
            (
                request.LockedVault.MaxSecondsBetweenExecutions != null
                && DateTimeOffset.UtcNow.ToUnixTimeSeconds() > (request.LockedVault.LastFarmedTimestamp + request.LockedVault.MaxSecondsBetweenExecutions.Value)
            )
            || DateTimeOffset.UtcNow.ToUnixTimeSeconds() > (request.LockedVault.LastFarmedTimestamp + request.AppConfig.AutoCompounderConfig.DefaultMaxSecondsBetweenExecutions)
            ;

            if (!isMaxSecondsBetweenExecution
                &&
                (
                    (
                        request.LockedVault.MinGasPercentOffsetToExecute != null
                        && pendingRewardValueInGas < totalTxFee.IncreasePercentage(request.LockedVault.MinGasPercentOffsetToExecute.Value)
                    )
                    || pendingRewardValueInGas < totalTxFee.IncreasePercentage(request.AppConfig.AutoCompounderConfig.DefaultMinProfitToGasPercentOffset)
                )
            )
            {
                this._logger.LogInformation("Canceled (execution not profitable).");
                return false;
            }

            float updatedGasPrice = await this._gasPriceService.GetStandardGasPrice(request.LockedVault.BlockchainId);

            if (updatedGasPrice < gasPrice)
            {
                gasPrice = updatedGasPrice;
                this._logger.LogInformation($"Updating execution gas limit (units) to: {executionGasEstimate}");
            }

            this._logger.LogInformation("Sending .Execute() transaction...");

            // TODO: Use Poly for retry. If it fails, increase the gas by {amount} offset.
            TransactionReceipt transactionReceipt = await currentStratServiceHandler.ExecuteRequestAndWaitForReceiptAsync(
                new ExecuteFunction()
                {
                    GasPrice = Web3.Convert.ToWei(gasPrice, UnitConversion.EthUnit.Gwei),
                    Gas = executionGasEstimate
                }
            );

            this._logger.LogInformation(".Execute() transaction ended.");

            this._logger.LogInformation($"Effective gas price: {transactionReceipt.EffectiveGasPrice.Value}");
            this._logger.LogInformation($"Gas used: {transactionReceipt.GasUsed.Value}");
            this._logger.LogInformation(
                "> Final total transaction fee: " +
                $"{Web3.Convert.FromWei(transactionReceipt.GasUsed.Value * transactionReceipt.EffectiveGasPrice.Value, UnitConversion.EthUnit.Gwei)}"
            );

            if (transactionReceipt.Failed())
            {
                this._logger.LogError(".Execute() transaction failed.");
                this._logger.LogError($"Logs: {transactionReceipt.Logs.ToString()}");
            }
            else
            {
                request.LockedVault.LastFarmedTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                this._logger.LogInformation("Success.");
            }

            await this._lockedVaultsStore.Update(request.LockedVault);

            request = null;

            return true;
        }

        private async Task<bool> IsToCancelExecution(AutoCompoundStrategy request, Web3 web3Client)
        {
            if (request.LockedVault.MinSecondsBetweenExecutions != null
                && DateTimeOffset.UtcNow.ToUnixTimeSeconds() < (request.LockedVault.LastFarmedTimestamp + request.LockedVault.MinSecondsBetweenExecutions)
            )
            {
                this._logger.LogInformation($"Cancelled ({nameof(request.LockedVault.MinSecondsBetweenExecutions)}).");
                return true;
            }

            if (request.LockedVault.StartTimestamp != null
                && DateTimeOffset.UtcNow.ToUnixTimeSeconds() < request.LockedVault.StartTimestamp
            )
            {
                this._logger.LogInformation($"Cancelled ({request.LockedVault.StartTimestamp}).");
                return true;
            }

            if (request.LockedVault.StartBlock != null)
            {
                BigInteger currentBlock = (await web3Client.Eth.Blocks.GetBlockNumber.SendRequestAsync()).Value;

                if (currentBlock < request.LockedVault.StartBlock)
                {
                    this._logger.LogInformation($"Cancelled ({nameof(request.LockedVault.StartBlock)}).");
                    return false;
                }
                else
                {
                    request.LockedVault.StartBlock = null;
                }
            }

            return false;
        }
    }
}
