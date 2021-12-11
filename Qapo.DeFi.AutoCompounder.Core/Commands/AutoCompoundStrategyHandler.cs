using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;

namespace Qapo.DeFi.AutoCompounder.Core.Commands
{
    public class AutoCompoundStrategyHandler : IRequestHandler<AutoCompoundStrategy, bool>
    {
        private readonly IConfigurationService _configurationService;

        private readonly ILoggerService _logger;

        public AutoCompoundStrategyHandler(IConfigurationService configurationService, ILoggerService logger)
        {
            this._configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            this._logger = logger ?? throw new ArgumentNullException(nameof(configurationService));
        }

        public Task<bool> Handle(AutoCompoundStrategy request, CancellationToken cancellationToken)
        {
            // > Check that the pending reward is {amount}% higher than the gas cost for execution.
            // > Check if the gas is currently too high. Cancel execution if it is.
            // > Send transaction with min gas cost.
            //   > Use Poly for retry. If it fails, increase the gas by {amount} offset.

            throw new NotImplementedException();
        }
    }
}
