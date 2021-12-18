using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Numerics;

using MediatR;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores;
using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;
using Qapo.DeFi.AutoCompounder.Core.Interfaces.Web3Services;
using Qapo.DeFi.AutoCompounder.Core.Interfaces.Web3Services.External;
using Qapo.DeFi.AutoCompounder.Core.Models.Config;
using Qapo.DeFi.AutoCompounder.Core.Models.Web3.External;
using Qapo.DeFi.AutoCompounder.Core.Models.Web3.LockedStratModels;
using Qapo.DeFi.AutoCompounder.Core.Factories;
using Qapo.DeFi.AutoCompounder.Core.Extensions;


namespace Qapo.DeFi.AutoCompounder.Core.Commands
{
    public class AutoCompoundStrategyHandler : IRequestHandler<AutoCompoundStrategy, bool>
    {
        private readonly IConfigurationService _configurationService;

        private readonly AppConfig _appConfig;

        private readonly ILockedVaultsStore _lockedVaultsStore;

        private readonly IBlockchainStore _blockchainStore;

        private readonly IDexStore _dexStore;

        private readonly ITokenStore _tokenStore;

        private readonly ILoggerService _logger;

        public AutoCompoundStrategyHandler(
            IConfigurationService configurationService,
            ILockedVaultsStore lockedVaultsStore,
            IBlockchainStore blockchainStore,
            IDexStore dexStore,
            ITokenStore tokenStore,
            ILoggerService logger
        )
        {
            this._configurationService = configurationService.ThrowIfNull(nameof(IConfigurationService));
            this._appConfig = this._configurationService.GetConfig<AppConfig>().GetAwaiter().GetResult();

            this._lockedVaultsStore = lockedVaultsStore.ThrowIfNull(nameof(ILockedVaultsStore));
            this._blockchainStore = blockchainStore.ThrowIfNull(nameof(IBlockchainStore));
            this._dexStore = dexStore.ThrowIfNull(nameof(IDexStore));
            this._tokenStore = tokenStore.ThrowIfNull(nameof(ITokenStore));
            this._logger = logger.ThrowIfNull(nameof(ILoggerService));
        }

        public async Task<bool> Handle(AutoCompoundStrategy request, CancellationToken cancellationToken)
        {
            if (request.LockedVault.SecondsOffsetBetweenExecutions != null && request.LockedVault.LastFarmedTimestamp != null
                && DateTimeOffset.UtcNow.ToUnixTimeSeconds() < (request.LockedVault.LastFarmedTimestamp + request.LockedVault.SecondsOffsetBetweenExecutions)
            )
            {
                return false;
            }

            if (request.LockedVault.StartTimestamp != null
                && DateTimeOffset.UtcNow.ToUnixTimeSeconds() < request.LockedVault.StartTimestamp
            )
            {
                return false;
            }

            Account account = new Account(this._appConfig.SecretsConfig.WalletPrivateKey);

            Web3 web3 = new Web3(
                account,
                await this._blockchainStore.GetRpcUrlByChainId(request.LockedVault.BlockchainId)
            );

            if (request.LockedVault.StartBlock != null)
            {
                BigInteger currentBlock = (await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync()).Value;

                if (currentBlock < request.LockedVault.StartBlock)
                {
                    return false;
                }
                else
                {
                    request.LockedVault.StartBlock = null;
                }
            }

            ILockedStratService currentStratServiceHandler = LockedStratServicesFactory.Get(
                request.LockedVault.LockedStratServiceType,
                web3,
                request.LockedVault.VaultAddress
            );

            IUniswapV2RouterService uniswapV2RouterServiceHandler = UniswapV2RouterServicesFactory.Get(
                request.LockedVault.UniswapV2RouterServiceType,
                web3,
                (await this._dexStore.GetById(request.LockedVault.DexId)).ThrowIfNull("_dexStore.GetById").UniswapV2RouterAddress
            );

            BigInteger pendingRewardAmount = await currentStratServiceHandler.GetPendingRewardAmountQueryAsync();

            string nativeTokenAddress = await this._tokenStore.GetAddressById(request.LockedVault.NativeAssetId);

            BigInteger pendingRewardValueInGas = (await uniswapV2RouterServiceHandler.GetAmountsOutQueryAsync(
                new GetAmountsOutFunction()
                {
                    AmountIn = pendingRewardAmount,
                    Path = new List<string>()
                    {
                        request.LockedVault.RewardAssetAddress,
                        nativeTokenAddress
                    }
                }
            ))[2];

            BigInteger executionCostEstimateInGas = (await currentStratServiceHandler.ContractHandler.EstimateGasAsync<ExecuteFunction>()).Value;

            if ((
                request.LockedVault.MinGasPercentOffsetToExecute != null
                && pendingRewardValueInGas < executionCostEstimateInGas.IncreasePercentage(request.LockedVault.MinGasPercentOffsetToExecute.Value)
                )
                || pendingRewardValueInGas < executionCostEstimateInGas.IncreasePercentage(_appConfig.AutoCompounderConfig.DefaultMinProfitToGasPercentOffset)
            )
            {
                // Cancel execution if it's not profitable.
                return false;
            }

            // TODO: Use Poly for retry. If it fails, increase the gas by {amount} offset.
            await currentStratServiceHandler.ExecuteRequestAndWaitForReceiptAsync(
                new ExecuteFunction()
                {
                    GasPrice = 30,
                    Gas = executionCostEstimateInGas
                });

            request.LockedVault.LastFarmedTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            await this._lockedVaultsStore.Update(request.LockedVault);

            return true;
        }
    }
}
