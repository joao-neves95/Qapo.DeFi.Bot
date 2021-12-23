using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;

namespace Qapo.DeFi.AutoCompounder.Infra.Services
{
    public class LocalFileConfigurationService : IConfigurationService
    {
        public Task<T> GetConfig<T>()
        {
            throw new NotImplementedException();
        }
    }
}
