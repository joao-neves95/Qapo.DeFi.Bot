using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores;
using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;
using Qapo.DeFi.AutoCompounder.Core.Interfaces.Web3Services;
using Qapo.DeFi.AutoCompounder.Core.Models.Config;
using Qapo.DeFi.AutoCompounder.Core.Factories;
using Qapo.DeFi.AutoCompounder.Core.Extensions;
using Qapo.DeFi.AutoCompounder.Core.Models.Web3.LockedStratModels;
using System.Numerics;

namespace Qapo.DeFi.AutoCompounder.Core.Commands
{
    public class AutoCompoundStrategyHandler : IRequestHandler<AutoCompoundStrategy, bool>
    {
        private readonly IConfigurationService _configurationService;

        private readonly AppConfig _appConfig;

        private readonly IBlockchainStore _blockchainStore;

        private readonly ILoggerService _logger;

        public AutoCompoundStrategyHandler(
            IConfigurationService configurationService,
            IBlockchainStore blockchainStore,
            ILoggerService logger
        )
        {
            this._configurationService = configurationService.ThrowIfNull(nameof(configurationService));
            this._appConfig = this._configurationService.GetConfig<AppConfig>();

            this._blockchainStore = blockchainStore.ThrowIfNull(nameof(blockchainStore));
            this._logger = logger.ThrowIfNull(nameof(logger));
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
                Web3ServiceType.SushiSwapLpLockedStratService,
                web3,
                request.LockedVault.VaultAddress
            );

            int pendingRewardValueInGas = (int)await currentStratServiceHandler.GetPendingRewardAmountQueryAsync();

            // TODO: Calculate estimated gas cost.
            int executionCostInGas = 0;

            if ((
                request.LockedVault.MinGasPercentOffsetToExecute != null
                && pendingRewardValueInGas < executionCostInGas.IncreasePercentage(request.LockedVault.MinGasPercentOffsetToExecute.Value)
                )
                || pendingRewardValueInGas < executionCostInGas.IncreasePercentage(_appConfig.AutoCompounderConfig.DefaultMinProfitToGasPercentOffset)
            )
            {
                // Cancel execution if it's not profitable.
                return false;
            }

            // > Check if the gas is currently too high. Cancel execution if it is.

            // TODO: Use Poly for retry. If it fails, increase the gas by {amount} offset.
            await currentStratServiceHandler.ExecuteRequestAndWaitForReceiptAsync(
                new ExecuteFunction()
                {
                    GasPrice = 30,
                    Gas = executionCostInGas
                });

            return true;
        }
    }
}
