using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores;
using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;
using Qapo.DeFi.AutoCompounder.Core.Models.Config;
using Qapo.DeFi.AutoCompounder.Core.Models.Data;
using Qapo.DeFi.AutoCompounder.Core.Extensions;

namespace Qapo.DeFi.AutoCompounder.Infra.Stores
{
    public class LockedVaultsFileStore : ILockedVaultsStore
    {
        private const string DbFileName = nameof(LockedVaultsFileStore);

        private readonly string _fileDbPath;

        private readonly IConfigurationService _configurationService;
        private readonly AppConfig _appConfig;

        public LockedVaultsFileStore(IConfigurationService configurationService)
        {
            this._configurationService = configurationService.ThrowIfNull(nameof(configurationService));
            this._appConfig = this._configurationService.GetConfig<AppConfig>();

            this._fileDbPath = Path.Combine(this._appConfig.LocalDataFilesPath, this._appConfig.LocalJsonDbFilesPath, $"{DbFileName}.json");

            if (!File.Exists(this._fileDbPath))
            {
                File.WriteAllText(this._fileDbPath, "[]", Encoding.UTF8);
            }
        }

        public async Task<List<LockedVault>> GetAll()
        {
            return await this.GetEntireList();
        }

        public async Task<LockedVault> GetByAddress(string vaultAddress)
        {
            List<LockedVault> allVaults = await this.GetEntireList();

            return allVaults.Find(lockedVault => lockedVault.VaultAddress == vaultAddress);
        }

        public async Task<LockedVault> UpdateByAddress(LockedVault updatedLockedVault)
        {
            return (await this.UpdateByAddress(new[] { updatedLockedVault }))?[0];
        }

        public async Task<List<LockedVault>> UpdateByAddress(LockedVault[] updatedLockedVaults)
        {
            List<LockedVault> allVaults = await this.GetAll();

            if (allVaults == null)
            {
                return null;
            }

            for (int i = 0; i < updatedLockedVaults.Length; ++i)
            {
                LockedVault vaultToUpdate = allVaults.Find(vault => vault.VaultAddress == updatedLockedVaults[i].VaultAddress);

                if (vaultToUpdate == null)
                {
                    continue;
                }

                vaultToUpdate.MinGasPercentOffsetToExecute = updatedLockedVaults[i].MinGasPercentOffsetToExecute;
                vaultToUpdate.TimestampOffsetBetweenExecutions = updatedLockedVaults[i].TimestampOffsetBetweenExecutions;
                vaultToUpdate.StartBlock = updatedLockedVaults[i].StartBlock;
                vaultToUpdate.StartTimestamp = updatedLockedVaults[i].StartTimestamp;
            }

            await this.SaveAll(allVaults);

            return updatedLockedVaults.ToList();
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
            await this.SaveAll(allVaults);

            return true;
        }

        private async Task<List<LockedVault>> GetEntireList()
        {
            string vaultArrayStr = await File.ReadAllTextAsync(this._fileDbPath);

            return JsonConvert.DeserializeObject<List<LockedVault>>(vaultArrayStr);
        }

        private async Task SaveAll(List<LockedVault> lockedVaults)
        {
            await File.WriteAllTextAsync(this._fileDbPath, JsonConvert.SerializeObject(lockedVaults, Formatting.None), Encoding.UTF8);
        }
    }
}
