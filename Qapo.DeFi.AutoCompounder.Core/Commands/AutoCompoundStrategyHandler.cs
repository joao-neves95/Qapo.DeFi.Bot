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

        private readonly IBlockchainStore _blockchainStore;

        private readonly IDexStore _dexStore;

        private readonly ITokenStore _tokenStore;

        private readonly ILoggerService _logger;

        public AutoCompoundStrategyHandler(
            IConfigurationService configurationService,
            IBlockchainStore blockchainStore,
            IDexStore dexStore,
            ITokenStore tokenStore,
            ILoggerService logger
        )
        {
            this._configurationService = configurationService.ThrowIfNull(nameof(IConfigurationService));
            this._appConfig = this._configurationService.GetConfig<AppConfig>().GetAwaiter().GetResult();

            this._blockchainStore = blockchainStore.ThrowIfNull(nameof(IBlockchainStore));
            this._dexStore = dexStore.ThrowIfNull(nameof(IDexStore));
            this._tokenStore = tokenStore.ThrowIfNull(nameof(ITokenStore));
            this._logger = logger.ThrowIfNull(nameof(ILoggerService));
        }

        public async Task<bool> Handle(AutoCompoundStrategy request, CancellationToken cancellationToken)
        {
            // throw new NotImplementedException();

            // web3 here is temp.
            Account account = new Account(this._appConfig.SecretsConfig.WalletPrivateKey);

            Web3 web3 = new Web3(
                account,
                await this._blockchainStore.GetRpcUrlByChainId(request.LockedVault.BlockchainId)
            );
            //

            ILockedStratService currentStratServiceHandler = LockedStratServicesFactory.Get(
                // TODO: Create a generic strategy.
                LockedStratServiceType.SushiSwapLpLockedStratService,
                web3,
                request.LockedVault.VaultAddress
            );

            IUniswapV2RouterService uniswapV2RouterServiceHandler = UniswapV2RouterServicesFactory.Get(
                // TODO: Create a generic strategy.
                UniswapV2RouterServiceType.SpookySwapV2RouterService,
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

            // TODO: Check if the gas is currently too high. Cancel execution if it is.

            // TODO: Use Poly for retry. If it fails, increase the gas by {amount} offset.
            await currentStratServiceHandler.ExecuteRequestAndWaitForReceiptAsync(
                new ExecuteFunction()
                {
                    GasPrice = 30,
                    Gas = executionCostEstimateInGas
                });

            return true;
        }
    }
}
