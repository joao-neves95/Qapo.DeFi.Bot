using System.Threading.Tasks;

using Qapo.DeFi.Bot.Core.Models.Data;

namespace Qapo.DeFi.Bot.Core.Interfaces.Stores
{
    public interface IDexStore : IStore<Dex>
    {
        Task<Dex> GetById(int id);
    }
}
