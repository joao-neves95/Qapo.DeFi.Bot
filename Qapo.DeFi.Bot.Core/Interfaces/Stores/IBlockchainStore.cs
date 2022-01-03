
using System.Threading.Tasks;

using Qapo.DeFi.Bot.Core.Models.Data;

namespace Qapo.DeFi.Bot.Core.Interfaces.Stores
{
    public interface IBlockchainStore : IStore<Blockchain>
    {
        Task<Blockchain> GetByChainId(int chainId);

        Task<string> GetRpcUrlByChainId(int chainId);

        Task<int> GetNativeAssetIdByChainId(int chainId);
    }
}
