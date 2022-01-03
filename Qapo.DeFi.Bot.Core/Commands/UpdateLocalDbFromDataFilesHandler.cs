using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Newtonsoft.Json;

using Qapo.DeFi.Bot.Core.Interfaces.Dto;
using Qapo.DeFi.Bot.Core.Interfaces.Services;
using Qapo.DeFi.Bot.Core.Interfaces.Stores;
using Qapo.DeFi.Bot.Core.Models.Config;
using Qapo.DeFi.Bot.Core.Models.Data;
using Qapo.DeFi.Bot.Core.Extensions;

namespace Qapo.DeFi.Bot.Core.Commands
{
    public class UpdateLocalDbFromDataFilesHandler : IRequestHandler<UpdateLocalDbFromDataFiles, bool>
    {
        private readonly IGeneralPersistenceStore _generalPersistenceStore;

        private readonly IBlockchainStore _blockchainStore;

        private readonly IDexStore _dexStore;

        private readonly ITokenStore _tokenStore;

        private readonly ILockedVaultsStore _lockedVaultsStore;

        private readonly ILoggerService _loggerService;

        public UpdateLocalDbFromDataFilesHandler(
            IGeneralPersistenceStore generalPersistenceStore,
            IBlockchainStore blockchainStore,
            IDexStore dexStore,
            ITokenStore tokenStore,
            ILockedVaultsStore lockedVaultsStore,
            ILoggerService loggerService
        )
        {
            this._generalPersistenceStore = generalPersistenceStore.ThrowIfNull(nameof(IGeneralPersistenceStore));
            this._blockchainStore = blockchainStore.ThrowIfNull(nameof(IBlockchainStore));
            this._dexStore = dexStore.ThrowIfNull(nameof(IDexStore));
            this._tokenStore = tokenStore.ThrowIfNull(nameof(ITokenStore));
            this._lockedVaultsStore = lockedVaultsStore.ThrowIfNull(nameof(ILockedVaultsStore));

            this._loggerService = loggerService.ThrowIfNull(nameof(ILoggerService));
        }

        public async Task<bool> Handle(UpdateLocalDbFromDataFiles request, CancellationToken cancellationToken)
        {
            this._loggerService.LogInformation($"Running {nameof(UpdateLocalDbFromDataFilesHandler)}...");
            this._loggerService.LogInformation("Updating DB");

            long lastUpdateTimestamp = await this._generalPersistenceStore.GetLastDbUpdateTimestamp();

            if (lastUpdateTimestamp > 0
                && DateTimeOffset.UtcNow.ToUnixTimeSeconds() < (lastUpdateTimestamp + request.AppConfig.SecondsBetweenDbUpdate)
            )
            {
                this._loggerService.LogInformation($"Cancelled the DB update ({nameof(lastUpdateTimestamp)})");
                return false;
            }

            request.AppConfig.ThrowIfNull(nameof(request.AppConfig));

            this._loggerService.LogInformation($"Updating {nameof(_blockchainStore)}");

            List<Blockchain> allUpdatedBlockchains = await ReadAllEntitiesToUpdate<Blockchain>(
                BuildDataFilePath(request.AppConfig, nameof(Blockchain))
            );

            List<int> allUpdatedBlockchainIds = allUpdatedBlockchains.ConvertAll(blockchain => blockchain.ChainId);

            List<int> allExistingBlockchainIds = (await this._blockchainStore.GetAll()).ConvertAll(chain => chain.ChainId);
            IEnumerable<int> blockchainToAddIds = allUpdatedBlockchainIds.Where(updatedChainId => !allExistingBlockchainIds.Contains(updatedChainId));
            IEnumerable<Blockchain> blockchainsToAdd = allUpdatedBlockchains.Where(updatedChain => blockchainToAddIds.Contains(updatedChain.ChainId));
            await this._blockchainStore.Add(blockchainsToAdd);

            await this._blockchainStore.Update(allUpdatedBlockchains);

            IEnumerable<Blockchain> blockchainsToRemove = (await this._blockchainStore.GetAll())
                .Where(blockchain => !allUpdatedBlockchainIds.Contains(blockchain.ChainId))
            ;

            await this._blockchainStore.Remove(blockchainsToRemove);

            this._loggerService.LogInformation($"Updating {nameof(_dexStore)}");

            List<Dex> allUpdatedDexs = await ReadAllEntitiesToUpdate<Dex>(
                BuildDataFilePath(request.AppConfig, nameof(Dex))
            );

            List<int> allUpdatedDexIds = allUpdatedDexs.ConvertAll(dex => dex.Id ?? -1);

            List<int> allExistingDexIds = (await this._dexStore.GetAll()).ConvertAll(dex => dex.Id ?? -1);
            IEnumerable<int> dexToAddIds = allUpdatedDexIds.Where(updatedDexId => !allExistingDexIds.Contains(updatedDexId));
            IEnumerable<Dex> dexsToAdd = allUpdatedDexs.Where(updatedDex => dexToAddIds.Contains(updatedDex.Id ?? -1));
            await this._dexStore.Add(dexsToAdd);

            await this._dexStore.Update(allUpdatedDexs);

            IEnumerable<Dex> dexsToRemove = (await this._dexStore.GetAll())
                .Where(dex => !allUpdatedDexIds.Contains(dex.Id ?? -1))
            ;

            await this._dexStore.Remove(dexsToRemove);

            this._loggerService.LogInformation($"Updating {nameof(_tokenStore)}");

            List<Token> allUpdatedTokens = await ReadAllEntitiesToUpdate<Token>(
                BuildDataFilePath(request.AppConfig, nameof(Token))
            );

            List<int> allUpdatedTokenIds = allUpdatedTokens.ConvertAll(token => token.Id ?? -1);

            List<int> allExistingTokenIds = (await this._tokenStore.GetAll()).ConvertAll(token => token.Id ?? -1);
            IEnumerable<int> tokenToAddIds = allUpdatedTokenIds.Where(updatedTokenId => !allExistingTokenIds.Contains(updatedTokenId));
            IEnumerable<Token> tokensToAdd = allUpdatedTokens.Where(updatedToken => tokenToAddIds.Contains(updatedToken.Id ?? -1));
            await this._tokenStore.Add(tokensToAdd);

            await this._tokenStore.Update(allUpdatedTokens);

            IEnumerable<Token> tokensToRemove = (await this._tokenStore.GetAll())
                .Where(token => !allUpdatedTokenIds.Contains(token.Id ?? -1))
            ;

            await this._tokenStore.Remove(tokensToRemove);

            this._loggerService.LogInformation($"Updating {nameof(_lockedVaultsStore)}");

            List<LockedVault> allUpdatedLockedVaults = await ReadAllEntitiesToUpdate<LockedVault>(
                BuildDataFilePath(request.AppConfig, nameof(LockedVault))
            );

            List<string> allUpdatedLockedVaultIds = allUpdatedLockedVaults.ConvertAll(lockedVault => lockedVault.VaultAddress);

            List<string> allExistingLockedVaultIds = (await this._lockedVaultsStore.GetAll()).ConvertAll(lockedVault => lockedVault.VaultAddress);
            IEnumerable<string> lockedVaultToAddIds = allUpdatedLockedVaultIds.Where(updatedVaultId => !allExistingLockedVaultIds.Contains(updatedVaultId));
            IEnumerable<LockedVault> lockedVaultsToAdd = allUpdatedLockedVaults.Where(updatedLockedVault => lockedVaultToAddIds.Contains(updatedLockedVault.VaultAddress));
            await this._lockedVaultsStore.Add(lockedVaultsToAdd);

            await this._lockedVaultsStore.Update(allUpdatedLockedVaults);

            IEnumerable<LockedVault> lockedVaulToRemove = (await this._lockedVaultsStore.GetAll())
                .Where(lockedVault => !allUpdatedLockedVaultIds.Contains(lockedVault.VaultAddress))
            ;

            await this._lockedVaultsStore.Remove(lockedVaulToRemove);

            await this._generalPersistenceStore.SetLastDbUpdateTimestampToNow();

            this._loggerService.LogInformation("");

            return true;

            // List<IStore<IEntity>> allStores = new List<IStore<IEntity>>
            // {
            //     this._blockchainStore,
            //     this._dexStore,
            //     this._tokenStore,
            //     this._lockedVaultsStore
            // };

            // for (int i = 0; i < allStores.Count; ++i)
            // {
            //     await this.UpdateDb(request, allStores[i]);
            // }
        }

        private async Task UpdateDb(UpdateLocalDbFromDataFiles request, IStore<IEntity> store)
        {
            throw new NotImplementedException();

            this._loggerService.LogInformation($"Updating {BuildDataFileName(typeof(IEntity).FullName)}");

            List<IEntity> allUpdatedEntities = await UpdateLocalDbFromDataFilesHandler
                .ReadAllEntitiesToUpdate<IEntity>(
                    BuildDataFilePath(request.AppConfig, typeof(IEntity).FullName)
                )
            ;

            await store.Update(allUpdatedEntities.ToArray());

            int[] allUpdatedEntityIds = allUpdatedEntities.Select(entity => entity.Id ?? -1).ToArray();

            IEntity[] entitiesToRemove = (await store.GetAll())
                .Where(entity => !allUpdatedEntityIds.Contains(entity.Id ?? -1))
                .ToArray()
            ;

            await store.Remove(entitiesToRemove);
        }

        private static string BuildDataFilePath(AppConfig appConfig, string entityName)
        {
            return Path.Combine(appConfig.LocalDataFilesPath, BuildDataFileName(entityName));
        }

        private static string BuildDataFileName(string entityName)
        {
            return $"{entityName.ToLower()}s.json";
        }

        private static async Task<List<TEntity>> ReadAllEntitiesToUpdate<TEntity>(string filePath)
        {
            return JsonConvert.DeserializeObject<List<TEntity>>(await File.ReadAllTextAsync(filePath, Encoding.UTF8));
        }
    }
}
