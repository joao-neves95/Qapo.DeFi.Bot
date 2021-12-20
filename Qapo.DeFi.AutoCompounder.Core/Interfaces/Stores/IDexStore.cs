using System.Threading.Tasks;

using Qapo.DeFi.AutoCompounder.Core.Models.Data;

namespace Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores
{
    public interface IDexStore : IStore<Dex>
    {
        Task<Dex> GetById(int id);
    }
}
