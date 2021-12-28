using System.Collections.Generic;
using System.Threading.Tasks;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Dto;

namespace Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores
{
    public interface IStore<TEntity>
        where TEntity : IEntity
    {
        Task<List<TEntity>> GetAll();

        Task<TEntity> Update(TEntity updatedEntity);

        Task<List<TEntity>> Update(IEnumerable<TEntity> updatedEntities);

        Task<bool> Remove(TEntity updatedEntities);

        Task<bool> Remove(IEnumerable<TEntity> updatedEntities);
    }
}
