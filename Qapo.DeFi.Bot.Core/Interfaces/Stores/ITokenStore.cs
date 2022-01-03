using System.Threading.Tasks;

using Qapo.DeFi.Bot.Core.Models.Data;

namespace Qapo.DeFi.Bot.Core.Interfaces.Stores
{
    public interface ITokenStore : IStore<Token>
    {
        Task<Token> GetById(int id);

        Task<string> GetAddressById(int id);
    }
}
