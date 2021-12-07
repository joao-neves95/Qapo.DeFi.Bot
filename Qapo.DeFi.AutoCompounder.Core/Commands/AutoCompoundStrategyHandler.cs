using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Qapo.DeFi.AutoCompounder.Core.Commands
{
    public class AutoCompoundStrategyHandler : IRequestHandler<AutoCompoundStrategy, bool>
    {
        public Task<bool> Handle(AutoCompoundStrategy request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
