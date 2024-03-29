using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Qapo.DeFi.Bot.Core.Interfaces.Dto;
using Qapo.DeFi.Bot.Core.Interfaces.Services;
using Qapo.DeFi.Bot.Infra.Stores;

namespace Qapo.DeFi.Bot.Infra.Services
{
    public class LocalFileConfigurationService<TConfig> : FileStoreBase<TConfig>, IConfigurationService<TConfig>
        where TConfig : IAppConfig
    {
        public LocalFileConfigurationService()
            : base()
        {
            // TODO: Make this path configurable.
            this.SetFileDbPath("./", "./_data/appConfig");
            this.EnsureCreated(string.Empty).GetAwaiter().GetResult();
        }

        public async Task<TConfig> GetConfig()
        {
            return await base.GetEntity<TConfig>();
        }
    }
}
