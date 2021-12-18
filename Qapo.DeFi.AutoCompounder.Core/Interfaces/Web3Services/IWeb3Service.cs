
using Nethereum.Contracts.ContractHandlers;

namespace Qapo.DeFi.AutoCompounder.Core.Interfaces.Web3Services
{
    public interface IWeb3Service
    {
        ContractHandler ContractHandler { get; }
    }
}
