using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;
using Qapo.DeFi.AutoCompounder.Core.Extensions;
using Qapo.DeFi.AutoCompounder.Core.Models.Config;

namespace Qapo.DeFi.AutoCompounder.Core.Commands
{
    public class AutoCompoundStrategyHandler : IRequestHandler<AutoCompoundStrategy, bool>
    {
        private readonly IConfigurationService _configurationService;

        private readonly AppConfig _appConfig;

        private readonly ILoggerService _logger;

        public AutoCompoundStrategyHandler(
            IConfigurationService configurationService,
            ILoggerService logger
        )
        {
            this._configurationService = configurationService.ThrowIfNull(nameof(configurationService));
            this._appConfig = this._configurationService.GetConfig<AppConfig>();

            this._logger = logger.ThrowIfNull(nameof(logger));
        }

        public Task<bool> Handle(AutoCompoundStrategy request, CancellationToken cancellationToken)
        {
            // > Check that the pending reward is {amount}% higher than the gas cost for execution.
            int pendingRewardValueInGas = 0;
            int executionCost = 0;

            if ((
                request.LockedVault.MinGasPercentOffsetToExecute != null
                && pendingRewardValueInGas < executionCost.IncreasePercentage(request.LockedVault.MinGasPercentOffsetToExecute.Value)
                )
                || pendingRewardValueInGas < executionCost.IncreasePercentage(_appConfig.AutoCompounderConfig.DefaultMinProfitToGasPercentOffset)
            )
            {
                // Cancel execution if it's not profitable.
                return Task.FromResult(false);
            }

            // > Check if the gas is currently too high. Cancel execution if it is.
            // > Send transaction with min gas cost.
            //   > Use Poly for retry. If it fails, increase the gas by {amount} offset.

            throw new NotImplementedException();
        }
    }
}
