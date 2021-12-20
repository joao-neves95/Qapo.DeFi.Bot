using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;
using Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores;
using Qapo.DeFi.AutoCompounder.Core.Models.Data;

namespace Qapo.DeFi.AutoCompounder.Infra.Stores
{
    public class BlockchainFileStore : FileStoreBase<Blockchain>, IBlockchainStore
    {
        public BlockchainFileStore(IConfigurationService configurationService)
            : base(configurationService, nameof(BlockchainFileStore))
        {
        }

        public async Task<Blockchain> GetByChainId(int chainId)
        {
            return (await base.GetAll())?.Find(blockchain => blockchain.ChainId == chainId);
        }

        public async Task<string> GetRpcUrlByChainId(int chainId)
        {
            return (await this.GetByChainId(chainId))?.RpcUrl;
        }

        public async Task<Blockchain> Update(Blockchain updatedBlockchain)
        {
            return (await this.Update(new[] { updatedBlockchain }))?[0];
        }

        public async Task<List<Blockchain>> Update(Blockchain[] updatedBlockchains)
        {
            List<Blockchain> allBlockchains = await base.GetAll();

            for (int i = 0; i < updatedBlockchains.Length; ++i)
            {
                Blockchain updatedBlockchain = updatedBlockchains[i];
                Blockchain blockchainToUpdate = allBlockchains?.Find(blockchain => blockchain.ChainId == updatedBlockchain.ChainId);

                if (blockchainToUpdate == null)
                {
                    continue;
                }

                blockchainToUpdate.ChainId = updatedBlockchain.ChainId;
                blockchainToUpdate.Name = updatedBlockchain.Name;
                blockchainToUpdate.RpcUrl = updatedBlockchain.RpcUrl;
            }

            await base.SaveAll(allBlockchains);

            return updatedBlockchains.ToList();
        }

        public async Task<bool> Remove(Blockchain blockchain)
        {
            return await this.Remove(new[] { blockchain });
        }

        public async Task<bool> Remove(Blockchain[] blockchains)
        {
            List<Blockchain> allBlockchains = await base.GetAll();

            if (allBlockchains == null)
            {
                return false;
            }

            IEnumerable<int> allChainIds = blockchains.Select(blockchain => blockchain.ChainId);

            allBlockchains.RemoveAll(blockchain => allChainIds.Contains(blockchain.ChainId));
            await base.SaveAll(allBlockchains);

            return true;
        }
    }
}
