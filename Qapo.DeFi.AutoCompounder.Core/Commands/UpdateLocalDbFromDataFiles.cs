using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

namespace Qapo.DeFi.AutoCompounder.Core.Commands
{
    public class UpdateLocalDbFromDataFiles : IRequest<bool>
    {
    }
}
