using MediatR;

using Qapo.DeFi.AutoCompounder.Core.Models.Data;

namespace Qapo.DeFi.AutoCompounder.Core.Commands
{
    public class AutoCompoundStrategy : IRequest<bool>
    {
        public LockedVault LockedVault { get; set; }
    }
}
