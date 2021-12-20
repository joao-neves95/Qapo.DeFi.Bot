using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores;
using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;
using Qapo.DeFi.AutoCompounder.Core.Models.Data;

namespace Qapo.DeFi.AutoCompounder.Infra.Stores
{
    public class DexFileStore : FileStoreBase<Dex>, IDexStore
    {
        public DexFileStore(IConfigurationService configurationService)
            : base(configurationService, nameof(DexFileStore))
        {
        }

        public async Task<Dex> GetById(int id)
        {
            return (await base.GetAll())?.Find(dex => dex.Id == id);
        }

        public async Task<Dex> Update(Dex updatedDex)
        {
            return (await this.Update(new[] { updatedDex }))?[0];
        }

        public async Task<List<Dex>> Update(Dex[] updatedDexs)
        {
            List<Dex> allDexs = await base.GetAll();

            if (allDexs == null)
            {
                return null;
            }

            for (int i = 0; i < updatedDexs.Length; ++i)
            {
                Dex updatedDex = updatedDexs[i];
                Dex dexToUpdate = allDexs?.Find(dex => dex.Id == updatedDex.Id);

                if (dexToUpdate == null)
                {
                    continue;
                }

                dexToUpdate.Name = updatedDex.Name;
                dexToUpdate.ChainId = updatedDex.ChainId;
                dexToUpdate.UniswapV2RouterAddress = updatedDex.UniswapV2RouterAddress;
                dexToUpdate.UniswapV2RouterServiceType = updatedDex.UniswapV2RouterServiceType;
            }

            await base.SaveAll(allDexs);

            return updatedDexs.ToList();
        }

        public async Task<bool> Remove(Dex dex)
        {
            return await this.Remove(new[] { dex });
        }

        public async Task<bool> Remove(Dex[] dex)
        {
            List<Dex> allDexs = await base.GetAll();
            IEnumerable<int> allDexIds = allDexs.Select(dex => dex.Id);

            allDexs.RemoveAll(dex => allDexIds.Contains(dex.Id));
            await base.SaveAll(allDexs);

            return true;
        }
    }
}
