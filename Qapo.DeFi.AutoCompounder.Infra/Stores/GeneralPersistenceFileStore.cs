using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;
using Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores;
using Qapo.DeFi.AutoCompounder.Core.Models.Config;
using Qapo.DeFi.AutoCompounder.Core.Models.Entities;

namespace Qapo.DeFi.AutoCompounder.Infra.Stores
{
    public class GeneralPersistenceFileStore : FileStoreBase<GeneralPersistence>, IGeneralPersistenceStore
    {
        public GeneralPersistenceFileStore(IConfigurationService<AppConfig> configurationService)
            : base(configurationService, nameof(GeneralPersistenceFileStore), string.Empty)
        {
        }

        public async Task<long> GetLastDbUpdateTimestamp()
        {
            GeneralPersistence generalPersistence = await base.GetEntity<GeneralPersistence>();
            return generalPersistence.LastDbUpdateTimestamp;
        }

        public async Task<long> SetLastDbUpdateTimestampToNow()
        {
            return await this.SetLastDbUpdateTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        }

        public async Task<long> SetLastDbUpdateTimestamp(long timestamp)
        {
            GeneralPersistence generalPersistence = await base.GetEntity<GeneralPersistence>();
            generalPersistence.LastDbUpdateTimestamp = timestamp;
            await base.Save(generalPersistence);

            return timestamp;
        }

        public Task<GeneralPersistence> Add(GeneralPersistence entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<GeneralPersistence>> Add(IEnumerable<GeneralPersistence> entities)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralPersistence> Update(GeneralPersistence updatedEntity)
        {
            throw new NotImplementedException();
        }

        public Task<List<GeneralPersistence>> Update(IEnumerable<GeneralPersistence> updatedEntities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(GeneralPersistence updatedEntities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(IEnumerable<GeneralPersistence> updatedEntities)
        {
            throw new NotImplementedException();
        }
    }
}
