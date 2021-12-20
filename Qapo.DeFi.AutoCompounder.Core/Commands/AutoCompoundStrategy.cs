using MediatR;

using Qapo.DeFi.AutoCompounder.Core.Models.Data;
using Qapo.DeFi.AutoCompounder.Core.Models.Config;

namespace Qapo.DeFi.AutoCompounder.Core.Commands
{
    public class AutoCompoundStrategy : IRequest<bool>
    {
        public LockedVault LockedVault { get; set; }

        public AppConfig AppConfig { get; set; }
    }
}
