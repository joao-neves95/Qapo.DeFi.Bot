using System.Threading.Tasks;

using Qapo.DeFi.AutoCompounder.Core.Models.Data;

namespace Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores
{
    public interface ITokenStore : IStore<Token>
    {
        Task<Token> GetById(int id);

        Task<string> GetAddressById(int id);
    }
}
