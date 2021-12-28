
using System.Threading.Tasks;

using Qapo.DeFi.AutoCompounder.Core.Models.Entities;

namespace Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores
{
    public interface IGeneralPersistenceStore : IStore<GeneralPersistence>
    {
        Task<long> GetLastDbUpdateTimestamp();

        Task<long> SetLastDbUpdateTimestampToNow();

        Task<long> SetLastDbUpdateTimestamp(long timestamp);
    }
}
