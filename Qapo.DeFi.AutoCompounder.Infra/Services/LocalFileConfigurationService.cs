using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Dto;
using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;
using Qapo.DeFi.AutoCompounder.Core.Models.Config;
using Qapo.DeFi.AutoCompounder.Core.Extensions;
using Qapo.DeFi.AutoCompounder.Infra.Stores;

namespace Qapo.DeFi.AutoCompounder.Infra.Services
{
    public class LocalFileConfigurationService<TConfig> : FileStoreBase<TConfig>, IConfigurationService<TConfig>
        where TConfig : IAppConfig
    {
        public LocalFileConfigurationService()
            : base()
        {
            this.SetFileDbPath("./", "config");
        }

        public async Task<TConfig> GetConfig()
        {
            return await base.GetEntity<TConfig>();
        }
    }
}
