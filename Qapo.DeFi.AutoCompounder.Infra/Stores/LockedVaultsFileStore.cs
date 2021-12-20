using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores;
using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;
using Qapo.DeFi.AutoCompounder.Core.Models.Data;

namespace Qapo.DeFi.AutoCompounder.Infra.Stores
{
    public class LockedVaultsFileStore : FileStoreBase<LockedVault>, ILockedVaultsStore
    {
        public LockedVaultsFileStore(IConfigurationService configurationService)
            : base(configurationService, nameof(LockedVaultsFileStore))
        {
        }

        public async Task<LockedVault> GetByAddress(string vaultAddress)
        {
            List<LockedVault> allVaults = await this.GetAll();

            return allVaults?.Find(lockedVault => lockedVault.VaultAddress == vaultAddress);
        }

        public async Task<LockedVault> Update(LockedVault updatedLockedVault)
        {
            return (await this.Update(new[] { updatedLockedVault }))?[0];
        }

        public async Task<List<LockedVault>> Update(LockedVault[] updatedLockedVaults)
        {
            List<LockedVault> allVaults = await base.GetAll();

            if (allVaults == null)
            {
                return null;
            }

            for (int i = 0; i < updatedLockedVaults.Length; ++i)
            {
                LockedVault updatedLockedVault = updatedLockedVaults[i];
                LockedVault vaultToUpdate = allVaults?.Find(vault => vault.VaultAddress == updatedLockedVault.VaultAddress);

                if (vaultToUpdate == null)
                {
                    continue;
                }

                vaultToUpdate.MinGasPercentOffsetToExecute = updatedLockedVault.MinGasPercentOffsetToExecute;
                vaultToUpdate.SecondsOffsetBetweenExecutions = updatedLockedVault.SecondsOffsetBetweenExecutions;
                vaultToUpdate.StartBlock = updatedLockedVault.StartBlock;
                vaultToUpdate.StartTimestamp = updatedLockedVault.StartTimestamp;
            }

            await base.SaveAll(allVaults);

            return updatedLockedVaults.ToList();
        }

        public async Task<bool> Remove(LockedVault lockedVault)
        {
            return await this.RemoveByAddress(lockedVault.VaultAddress);
        }

        public async Task<bool> Remove(LockedVault[] lockedVaults)
        {
            return await this.RemoveByAddress(lockedVaults.Select(vault => vault.VaultAddress).ToArray());
        }

        public async Task<bool> RemoveByAddress(string vaultAddress)
        {
            return await this.RemoveByAddress(new[] { vaultAddress });
        }

        public async Task<bool> RemoveByAddress(string[] vaultAddresses)
        {
            List<LockedVault> allVaults = await this.GetAll();

            if (allVaults == null)
            {
                return false;
            }

            allVaults.RemoveAll(vault => vaultAddresses.Contains(vault.VaultAddress));
            await base.SaveAll(allVaults);

            return true;
        }
    }
}
