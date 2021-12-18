using System.Threading.Tasks;

namespace Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores
{
    public interface ITokenStore
    {
        Task<string> GetAddressById(int id);
    }
}
