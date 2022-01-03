using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Qapo.DeFi.Bot.Core.Interfaces.Dto;
using Qapo.DeFi.Bot.Core.Interfaces.Services;
using Qapo.DeFi.Bot.Core.Models.Config;
using Qapo.DeFi.Bot.Core.Extensions;

namespace Qapo.DeFi.Bot.Infra.Stores
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

        protected FileStoreBase(
            IConfigurationService<AppConfig> configurationService,
            string dbFileName,
            string initialValue = "[]"
        )
        {
            this._configurationService = configurationService.ThrowIfNull(nameof(configurationService));
            this._appConfig = this._configurationService.GetConfig().GetAwaiter().GetResult();

            this.SetFileDbPath(this._appConfig.LocalJsonDbFilesPath, dbFileName);

            this.EnsureCreated(initialValue).GetAwaiter().GetResult();
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

        protected async Task<List<TEntity>> GetEntireEntityList()
        {
            return await this.GetEntity<List<TEntity>>();
        }

        protected async Task<T> GetEntity<T>()
        {
            string jsonStr = await File.ReadAllTextAsync(this.FileDbPath);

            return JsonConvert.DeserializeObject<T>(jsonStr);
        }

        protected async Task SaveAll(List<TEntity> entities)
        {
            await this.Save(entities);
        }

        protected async Task Save<T>(T entity)
        {
            await File.WriteAllTextAsync(this.FileDbPath, JsonConvert.SerializeObject(entity, Formatting.None), Encoding.UTF8);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await this.GetEntireEntityList();
        }
    }
}
