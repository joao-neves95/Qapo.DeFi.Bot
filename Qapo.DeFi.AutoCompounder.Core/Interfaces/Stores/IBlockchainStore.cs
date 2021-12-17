
using System.Threading.Tasks;

namespace Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores
{
    public interface IBlockchainStore
    {
        Task<string> GetRpcUrlByChainId(int chainId);
    }
}
