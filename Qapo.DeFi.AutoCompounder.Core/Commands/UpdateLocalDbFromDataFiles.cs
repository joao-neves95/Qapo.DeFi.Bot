using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using Qapo.DeFi.AutoCompounder.Core.Models.Config;

namespace Qapo.DeFi.AutoCompounder.Core.Commands
{
    public class UpdateLocalDbFromDataFiles : IRequest<bool>
    {
        public AppConfig AppConfig { get; set; }
    }
}
