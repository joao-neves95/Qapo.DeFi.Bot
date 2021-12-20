using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;
using Qapo.DeFi.AutoCompounder.Core.Models.Config;
using Qapo.DeFi.AutoCompounder.Core.Extensions;

namespace Qapo.DeFi.AutoCompounder.Infra.Stores
{
    public abstract class FileStoreBase<TEntity>
    {
        protected readonly IConfigurationService _configurationService;

        protected readonly AppConfig _appConfig;

        protected readonly string _fileDbPath;

        protected readonly string _dbFileName;

        protected FileStoreBase(IConfigurationService configurationService, string dbFileName)
        {
            this._configurationService = configurationService.ThrowIfNull(nameof(configurationService));
            this._appConfig = this._configurationService.GetConfig<AppConfig>().GetAwaiter().GetResult();

            this._dbFileName = dbFileName;
            this._fileDbPath = Path.Combine(this._appConfig.LocalDataFilesPath, this._appConfig.LocalJsonDbFilesPath, $"{_dbFileName}.json");

            this.EnsureCreated().GetAwaiter().GetResult();
        }

        protected async Task EnsureCreated()
        {
            if (!File.Exists(this._fileDbPath))
            {
                await File.WriteAllTextAsync(this._fileDbPath, "[]", Encoding.UTF8);
            }
        }

        protected async Task<List<TEntity>> GetEntireEntityList()
        {
            string vaultArrayStr = await File.ReadAllTextAsync(this._fileDbPath);

            return JsonConvert.DeserializeObject<List<TEntity>>(vaultArrayStr);
        }

        protected async Task SaveAll(List<TEntity> lockedVaults)
        {
            await File.WriteAllTextAsync(this._fileDbPath, JsonConvert.SerializeObject(lockedVaults, Formatting.None), Encoding.UTF8);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await this.GetEntireEntityList();
        }
    }
}
