using System.Threading.Tasks;

using Qapo.DeFi.AutoCompounder.Core.Models.Data;

namespace Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores
{
    public interface IDexStore
    {
        Task<Dex> GetById(int id);
    }
}
