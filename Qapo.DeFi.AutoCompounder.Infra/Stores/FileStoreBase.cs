using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Dto;
using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;
using Qapo.DeFi.AutoCompounder.Core.Models.Config;
using Qapo.DeFi.AutoCompounder.Core.Extensions;

namespace Qapo.DeFi.AutoCompounder.Infra.Stores
{
    public abstract class FileStoreBase<TEntity>
    {
        protected readonly IConfigurationService<AppConfig> _configurationService;

        protected readonly IAppConfig _appConfig;

        protected string DbFileName { get; private set; }

        protected string FileDbPath { get; private set; }

        protected FileStoreBase()
        {
        }

        protected FileStoreBase(IConfigurationService<AppConfig> configurationService, string dbFileName)
        {
            this._configurationService = configurationService.ThrowIfNull(nameof(configurationService));
            this._appConfig = this._configurationService.GetConfig().GetAwaiter().GetResult();

            this.SetFileDbPath(this._appConfig.LocalJsonDbFilesPath, dbFileName);

            this.EnsureCreated().GetAwaiter().GetResult();
        }

        protected void SetFileDbPath(string fileDbPath, string dbFileName)
        {
            this.DbFileName = dbFileName;
            this.FileDbPath = Path.Combine(fileDbPath, $"{dbFileName}.json");
        }

        protected async Task EnsureCreated(string initialValue = "[]")
        {
            if (!File.Exists(this.FileDbPath))
            {
                await File.WriteAllTextAsync(this.FileDbPath, initialValue, Encoding.UTF8);
            }
        }

        protected async Task<TEntity> GetEntity()
        {
            return await this.GetEntity<TEntity>();
        }

        protected async Task<T> GetEntity<T>()
        {
            string jsonStr = await File.ReadAllTextAsync(this.FileDbPath);

            return JsonConvert.DeserializeObject<T>(jsonStr);
        }

        protected async Task<List<TEntity>> GetEntireEntityList()
        {
            string jsonStr = await File.ReadAllTextAsync(this.FileDbPath);

            return JsonConvert.DeserializeObject<List<TEntity>>(jsonStr);
        }

        protected async Task SaveAll(List<TEntity> entities)
        {
            await File.WriteAllTextAsync(this.FileDbPath, JsonConvert.SerializeObject(entities, Formatting.None), Encoding.UTF8);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await this.GetEntireEntityList();
        }
    }
}
