
using MediatR;

using Qapo.DeFi.Bot.Core.Models.Config;

namespace Qapo.DeFi.Bot.Core.Commands
{
    public class UpdateLocalDbFromDataFiles : IRequest<bool>
    {
        public AppConfig AppConfig { get; set; }
    }
}
