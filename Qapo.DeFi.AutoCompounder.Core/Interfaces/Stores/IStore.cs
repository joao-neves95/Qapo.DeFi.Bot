using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores
{
    public interface IStore<TEntity>
    {
        Task<List<TEntity>> GetAll();

        Task<TEntity> Update(TEntity updatedEntity);

        Task<List<TEntity>> Update(TEntity[] updatedEntities);

        Task<bool> Remove(TEntity updatedEntities);

        Task<bool> Remove(TEntity[] updatedEntities);
    }
}
