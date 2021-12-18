using System;

using Nethereum.Web3;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Web3Services.External;
using Qapo.DeFi.AutoCompounder.Core.Web3Services.External.SpookySwapV2RouterService;

namespace Qapo.DeFi.AutoCompounder.Core.Factories
{
    public static class UniswapV2RouterServicesFactory
    {
        public static IUniswapV2RouterService Get(UniswapV2RouterServiceType uniswapV2RouterServiceType, Web3 web3, string contractAddress)
        {
            return uniswapV2RouterServiceType switch
            {
                UniswapV2RouterServiceType.SpookySwapV2RouterService => new SpookySwapV2RouterService(web3, contractAddress),

                _ => throw new TypeAccessException("Unknown type: " + Enum.GetName(typeof(UniswapV2RouterServiceType), uniswapV2RouterServiceType))
            };
        }
    }
}
