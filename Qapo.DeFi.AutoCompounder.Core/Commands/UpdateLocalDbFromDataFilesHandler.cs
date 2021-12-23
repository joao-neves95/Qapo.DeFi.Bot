using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Newtonsoft.Json;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores;
using Qapo.DeFi.AutoCompounder.Core.Models.Config;
using Qapo.DeFi.AutoCompounder.Core.Models.Data;
using Qapo.DeFi.AutoCompounder.Core.Extensions;
using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;

namespace Qapo.DeFi.AutoCompounder.Core.Commands
{
    public class UpdateLocalDbFromDataFilesHandler : IRequestHandler<UpdateLocalDbFromDataFiles, bool>
    {
        private readonly IBlockchainStore _blockchainStore;

        private readonly IDexStore _dexStore;

        private readonly ITokenStore _tokenStore;

        private readonly ILockedVaultsStore _lockedVaultsStore;

        private readonly ILoggerService _loggerService;

        public UpdateLocalDbFromDataFilesHandler(
            IBlockchainStore blockchainStore,
            IDexStore dexStore,
            ITokenStore tokenStore,
            ILockedVaultsStore lockedVaultsStore,
            ILoggerService loggerService
        )
        {
            this._blockchainStore = blockchainStore.ThrowIfNull(nameof(IBlockchainStore));
            this._dexStore = dexStore.ThrowIfNull(nameof(IDexStore));
            this._tokenStore = tokenStore.ThrowIfNull(nameof(ITokenStore));
            this._lockedVaultsStore = lockedVaultsStore.ThrowIfNull(nameof(ILockedVaultsStore));

            this._loggerService = loggerService.ThrowIfNull(nameof(ILoggerService));
        }

        public async Task<bool> Handle(UpdateLocalDbFromDataFiles request, CancellationToken cancellationToken)
        {
            // TODO: Create a generic method.

            this._loggerService.LogInformation($"Running {nameof(UpdateLocalDbFromDataFilesHandler)}...");
            this._loggerService.LogInformation($"Updating DB");

            request.AppConfig.ThrowIfNull(nameof(request.AppConfig));

            this._loggerService.LogInformation($"Updating {nameof(_blockchainStore)}");

            List<Blockchain> allUpdatedBlockchains = await ReadAllEntitiesToUpdate<Blockchain>(
                BuildDataFilePath(request.AppConfig, nameof(Blockchain))
            );

            await this._blockchainStore.Update(allUpdatedBlockchains.ToArray());

            int[] allUpdatedBlockchainIds = allUpdatedBlockchains.Select(blockchain => blockchain.ChainId).ToArray();

            Blockchain[] blockchainsToRemove = (await this._blockchainStore.GetAll())
                .Where(blockchain => !allUpdatedBlockchainIds.Contains(blockchain.ChainId))
                .ToArray();

            await this._blockchainStore.Remove(blockchainsToRemove);

            this._loggerService.LogInformation($"Updating {nameof(_dexStore)}");

            List<Dex> allUpdatedDexs = await ReadAllEntitiesToUpdate<Dex>(
                BuildDataFilePath(request.AppConfig, nameof(Dex))
            );

            await this._dexStore.Update(allUpdatedDexs.ToArray());

            int[] allUpdatedDexIds = allUpdatedDexs.Select(dex => dex.Id).ToArray();

            Dex[] dexsToRemove = (await this._dexStore.GetAll())
                .Where(dex => !allUpdatedDexIds.Contains(dex.Id))
                .ToArray();

            await this._dexStore.Remove(dexsToRemove);

            this._loggerService.LogInformation($"Updating {nameof(_tokenStore)}");

            List<Token> allUpdatedTokens = await ReadAllEntitiesToUpdate<Token>(
                BuildDataFilePath(request.AppConfig, nameof(Token))
            );

            await this._tokenStore.Update(allUpdatedTokens.ToArray());

            int[] allUpdatedTokenIds = allUpdatedTokens.Select(token => token.Id).ToArray();

            Token[] tokensToRemove = (await this._tokenStore.GetAll())
                .Where(token => !allUpdatedTokenIds.Contains(token.Id))
                .ToArray();

            await this._tokenStore.Remove(tokensToRemove);

            this._loggerService.LogInformation($"Updating {nameof(_lockedVaultsStore)}");

            List<LockedVault> allUpdatedLockedVaults = await ReadAllEntitiesToUpdate<LockedVault>(
                BuildDataFilePath(request.AppConfig, nameof(LockedVault))
            );

            await this._lockedVaultsStore.Update(allUpdatedLockedVaults.ToArray());

            string[] allUpdatedLockedVaultIds = allUpdatedLockedVaults.Select(lockedVault => lockedVault.VaultAddress).ToArray();

            LockedVault[] lockedVaulToRemove = (await this._lockedVaultsStore.GetAll())
                .Where(lockedVault => !allUpdatedLockedVaultIds.Contains(lockedVault.VaultAddress))
                .ToArray();

            await this._lockedVaultsStore.Remove(lockedVaulToRemove);

            this._loggerService.LogInformation("");

            return true;
        }

        private static string BuildDataFilePath(AppConfig appConfig, string entityName)
        {
            return Path.Combine(appConfig.LocalDataFilesPath, BuildDataFileName(entityName));
        }

        private static string BuildDataFileName(string entityName)
        {
            return $"{entityName}s";
        }

        private static async Task<List<TEntity>> ReadAllEntitiesToUpdate<TEntity>(string filePath)
        {
            return JsonConvert.DeserializeObject<List<TEntity>>(await File.ReadAllTextAsync(filePath, Encoding.UTF8));
        }
    }
}
