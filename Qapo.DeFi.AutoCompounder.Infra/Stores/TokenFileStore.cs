using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores;
using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;
using Qapo.DeFi.AutoCompounder.Core.Models.Config;
using Qapo.DeFi.AutoCompounder.Core.Models.Data;

namespace Qapo.DeFi.AutoCompounder.Infra.Stores
{
    public class TokenFileStore : FileStoreBase<Token>, ITokenStore
    {
        public TokenFileStore(IConfigurationService<AppConfig> configurationService)
            : base(configurationService, nameof(TokenFileStore))
        {
        }

        public async Task<Token> GetById(int id)
        {
            return (await base.GetAll())?.Find(token => token.Id == id);
        }

        public async Task<string> GetAddressById(int id)
        {
            return (await GetById(id))?.Address;
        }

        public Task<Token> Update(Token updatedToken)
        public async Task<Token> Update(Token updatedToken)
        {
            return (await this.Update(new[] { updatedToken }))[0];
        }

        public async Task<List<Token>> Update(IEnumerable<Token> updatedTokens)
        {
            List<Token> allTokens = await base.GetAll();

            if (allTokens == null)
            {
                return null;
            }

            for (int i = 0; i < updatedTokens.Count(); ++i)
            {
                Token updatedToken = updatedTokens.ElementAtOrDefault(i);
                Token tokenToUpdate = allTokens?.Find(token => token.Id == updatedToken.Id);

                if (tokenToUpdate == null)
                {
                    updatedToken.Id = -1;
                    continue;
                }

                tokenToUpdate.Address = updatedToken.Address;
                tokenToUpdate.Name = updatedToken.Name;
                tokenToUpdate.Symbol = updatedToken.Symbol;
                tokenToUpdate.Decimals = updatedToken.Decimals;
                tokenToUpdate.ChainId = updatedToken.ChainId;
                tokenToUpdate.LogoUrl = updatedToken.LogoUrl;
            }

            await base.SaveAll(allTokens);

            return updatedTokens.ToList();
        }

        public async Task<bool> Remove(Token token)
        {
            return await this.Remove(new[] { token });
        }

        public async Task<bool> Remove(IEnumerable<Token> tokens)
        {
            if (!tokens.Any())
            {
                return false;
            }

            List<Token> allTokens = await base.GetAll();
            IEnumerable<int> allTokenIds = allTokens.Select(token => token.Id ?? -1);

            allTokens.RemoveAll(token => allTokenIds.Contains(token.Id ?? -1));
            await base.SaveAll(allTokens);

            return true;
        }
    }
}
