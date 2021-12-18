using System.Collections.Generic;
using System.Threading.Tasks;

using Qapo.DeFi.AutoCompounder.Core.Models.Data;

namespace Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores
{
    public interface ILockedVaultsStore
    {
        Task<List<LockedVault>> GetAll();

        Task<LockedVault> GetByAddress(string vaultAddress);

        Task<LockedVault> Update(LockedVault updatedLockedVault);

        Task<List<LockedVault>> Update(LockedVault[] updatedLockedVaults);

        Task<bool> RemoveByAddress(string vaultAddress);

        Task<bool> RemoveByAddress(string[] vaultAddresses);
    }
}
