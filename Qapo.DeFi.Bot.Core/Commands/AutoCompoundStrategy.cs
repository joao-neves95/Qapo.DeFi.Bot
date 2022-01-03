using MediatR;

using Qapo.DeFi.Bot.Core.Models.Data;
using Qapo.DeFi.Bot.Core.Models.Config;

namespace Qapo.DeFi.Bot.Core.Commands
{
    public class AutoCompoundStrategy : IRequest<bool>
    {
        public LockedVault LockedVault { get; set; }

        public AppConfig AppConfig { get; set; }
    }
}
