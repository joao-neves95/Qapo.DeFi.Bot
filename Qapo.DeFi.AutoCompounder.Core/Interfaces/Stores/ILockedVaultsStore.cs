using System.Collections.Generic;
using System.Threading.Tasks;

using Qapo.DeFi.AutoCompounder.Core.Models.Data;

namespace Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores
{
    public interface ILockedVaultsStore : IStore<LockedVault>
    {
        Task<LockedVault> GetByAddress(string vaultAddress);

        Task<bool> RemoveByAddress(string vaultAddress);

        Task<bool> RemoveByAddress(string[] vaultAddresses);
    }
}
