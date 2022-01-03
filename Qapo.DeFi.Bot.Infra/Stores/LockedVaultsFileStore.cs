using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Qapo.DeFi.Bot.Core.Interfaces.Stores;
using Qapo.DeFi.Bot.Core.Interfaces.Services;
using Qapo.DeFi.Bot.Core.Models.Config;
using Qapo.DeFi.Bot.Core.Models.Data;

namespace Qapo.DeFi.Bot.Infra.Stores
{
    public class LockedVaultsFileStore : FileStoreBase<LockedVault>, ILockedVaultsStore
    {
        public LockedVaultsFileStore(IConfigurationService<AppConfig> configurationService)
            : base(configurationService, nameof(LockedVaultsFileStore))
        {
        }

        public async Task<LockedVault> GetByAddress(string vaultAddress)
        {
            List<LockedVault> allVaults = await this.GetAll();

            return allVaults?.Find(lockedVault => lockedVault.VaultAddress == vaultAddress);
        }

        public async Task<LockedVault> Add(LockedVault entity)
        {
            return (await this.Add(new[] { entity }))?[0];
        }

        public async Task<List<LockedVault>> Add(IEnumerable<LockedVault> entities)
        {
            List<LockedVault> allLockedVaults = await base.GetAll();

            for (int i = 0 ; i < entities.Count(); ++i)
            {
                LockedVault newLockedVault = entities.ElementAt(i);
                LockedVault existingLockedVault = allLockedVaults.Find(lockedVault => lockedVault.VaultAddress == newLockedVault.VaultAddress);

                if (existingLockedVault != null)
                {
                    continue;
                }

                allLockedVaults.Add(newLockedVault);
            }

            await base.SaveAll(allLockedVaults);

            return entities.ToList();
        }

        public async Task<LockedVault> Update(LockedVault updatedLockedVault)
        {
            return (await this.Update(new[] { updatedLockedVault }))?[0];
        }

        public async Task<List<LockedVault>> Update(IEnumerable<LockedVault> updatedLockedVaults)
        {
            List<LockedVault> allVaults = await base.GetAll();

            if (allVaults == null)
            {
                return null;
            }

            for (int i = 0; i < updatedLockedVaults.Count(); ++i)
            {
                LockedVault updatedLockedVault = updatedLockedVaults.ElementAtOrDefault(i);
                LockedVault vaultToUpdate = allVaults?.Find(vault => vault.VaultAddress == updatedLockedVault.VaultAddress);

                if (vaultToUpdate == null)
                {
                    updatedLockedVault.VaultAddress = null;
                    continue;
                }

                vaultToUpdate.LockedStratServiceType = updatedLockedVault.LockedStratServiceType;
                vaultToUpdate.Name = updatedLockedVault.Name;
                vaultToUpdate.BlockchainId = updatedLockedVault.BlockchainId;
                vaultToUpdate.UnderlyingAssetAddress = updatedLockedVault.UnderlyingAssetAddress;
                vaultToUpdate.RewardAssetAddress = updatedLockedVault.RewardAssetAddress;
                vaultToUpdate.DexId = updatedLockedVault.DexId;
                vaultToUpdate.PoolId = updatedLockedVault.PoolId;
                vaultToUpdate.MinGasPercentOffsetToExecute = updatedLockedVault.MinGasPercentOffsetToExecute;
                vaultToUpdate.MinSecondsBetweenExecutions = updatedLockedVault.MinSecondsBetweenExecutions;
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

        public async Task<bool> Remove(IEnumerable<LockedVault> lockedVaults)
        {
            return await this.RemoveByAddress(lockedVaults.Select(vault => vault.VaultAddress));
        }

        public async Task<bool> RemoveByAddress(string vaultAddress)
        {
            return await this.RemoveByAddress(new[] { vaultAddress });
        }

        public async Task<bool> RemoveByAddress(IEnumerable<string> vaultAddresses)
        {
            if (!vaultAddresses.Any())
            {
                return false;
            }

            List<LockedVault> allVaults = await this.GetAll();

            if (allVaults == null)
            {
                return false;
            }

            allVaults.RemoveAll(vault => vaultAddresses.Contains(vault.VaultAddress));
            await base.SaveAll(allVaults);

            return true;
        }

        public Task<bool> RemoveByAddress(string[] vaultAddresses)
        {
            throw new NotImplementedException();
        }
    }
}
