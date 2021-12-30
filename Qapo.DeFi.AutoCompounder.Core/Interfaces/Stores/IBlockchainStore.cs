
using System.Threading.Tasks;

using Qapo.DeFi.AutoCompounder.Core.Models.Data;

namespace Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores
{
    public interface IBlockchainStore : IStore<Blockchain>
    {
        Task<Blockchain> GetByChainId(int chainId);

        Task<string> GetRpcUrlByChainId(int chainId);

        Task<int> GetNativeAssetIdByChainId(int chainId);
    }
}
