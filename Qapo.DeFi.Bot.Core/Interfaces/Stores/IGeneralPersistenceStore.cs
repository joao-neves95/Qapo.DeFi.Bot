
using System.Threading.Tasks;

using Qapo.DeFi.Bot.Core.Models.Entities;

namespace Qapo.DeFi.Bot.Core.Interfaces.Stores
{
    public interface IGeneralPersistenceStore : IStore<GeneralPersistence>
    {
        Task<long> GetLastDbUpdateTimestamp();

        Task<long> SetLastDbUpdateTimestampToNow();

        Task<long> SetLastDbUpdateTimestamp(long timestamp);
    }
}
