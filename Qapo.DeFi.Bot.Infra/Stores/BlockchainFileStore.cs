using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Qapo.DeFi.Bot.Core.Interfaces.Services;
using Qapo.DeFi.Bot.Core.Interfaces.Stores;
using Qapo.DeFi.Bot.Core.Models.Data;
using Qapo.DeFi.Bot.Core.Models.Config;

namespace Qapo.DeFi.Bot.Infra.Stores
{
    public class BlockchainFileStore : FileStoreBase<Blockchain>, IBlockchainStore
    {
        public BlockchainFileStore(IConfigurationService<AppConfig> configurationService)
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

        public async Task<int> GetNativeAssetIdByChainId(int chainId)
        {
            return (await this.GetByChainId(chainId))?.NativeTokenId ?? -1;
        }

        public async Task<Blockchain> Add(Blockchain entity)
        {
            return (await this.Add(new[] { entity }))?[0];
        }

        public async Task<List<Blockchain>> Add(IEnumerable<Blockchain> entities)
        {
            List<Blockchain> allBlockchains = await base.GetAll();

            for (int i = 0 ; i < entities.Count(); ++i)
            {
                Blockchain newBlockchain = entities.ElementAt(i);
                Blockchain existingBlockchain = allBlockchains.Find(blockchain => blockchain.Id == newBlockchain.Id);

                if (existingBlockchain != null)
                {
                    continue;
                }

                allBlockchains.Add(newBlockchain);
            }

            await base.SaveAll(allBlockchains);

            return entities.ToList();
        }

        public async Task<Blockchain> Update(Blockchain updatedBlockchain)
        {
            return (await this.Update(new[] { updatedBlockchain }))?[0];
        }

        public async Task<List<Blockchain>> Update(IEnumerable<Blockchain> updatedBlockchains)
        {
            List<Blockchain> allBlockchains = await base.GetAll();

            for (int i = 0; i < updatedBlockchains.Count(); ++i)
            {
                Blockchain updatedBlockchain = updatedBlockchains.ElementAtOrDefault(i);
                Blockchain blockchainToUpdate = allBlockchains?.Find(blockchain => blockchain.ChainId == updatedBlockchain.ChainId);

                if (blockchainToUpdate == null)
                {
                    updatedBlockchain.ChainId = -1;
                    continue;
                }

                blockchainToUpdate.ChainId = updatedBlockchain.ChainId;
                blockchainToUpdate.Name = updatedBlockchain.Name;
                blockchainToUpdate.RpcUrl = updatedBlockchain.RpcUrl;
                blockchainToUpdate.NativeTokenId = updatedBlockchain.NativeTokenId;
            }

            await base.SaveAll(allBlockchains);

            return updatedBlockchains.ToList();
        }

        public async Task<bool> Remove(Blockchain blockchain)
        {
            return await this.Remove(new[] { blockchain });
        }

        public async Task<bool> Remove(IEnumerable<Blockchain> blockchains)
        {
            if (!blockchains.Any())
            {
                return false;
            }

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
