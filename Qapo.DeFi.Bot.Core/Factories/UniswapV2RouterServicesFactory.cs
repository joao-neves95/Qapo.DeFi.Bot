using System;

using Nethereum.Web3;

using Qapo.DeFi.Bot.Core.Interfaces.Web3Services.External;
using Qapo.DeFi.Bot.Core.Web3Services.External;

namespace Qapo.DeFi.Bot.Core.Factories
{
    public static class UniswapV2RouterServicesFactory
    {
        public static IUniswapV2RouterService Get(string uniswapV2RouterServiceTypeStr, Web3 web3, string contractAddress)
        {
            return UniswapV2RouterServicesFactory.Get(
                (UniswapV2RouterServiceType)Enum.Parse(typeof(UniswapV2RouterServiceType), uniswapV2RouterServiceTypeStr),
                web3,
                contractAddress
            );
        }

        public static IUniswapV2RouterService Get(UniswapV2RouterServiceType uniswapV2RouterServiceType, Web3 web3, string contractAddress)
        {
            return uniswapV2RouterServiceType switch
            {
                UniswapV2RouterServiceType.UniswapV2RouterService => new UniswapV2RouterService(web3, contractAddress),

                _ => throw new TypeAccessException("Unknown type: " + Enum.GetName(typeof(UniswapV2RouterServiceType), uniswapV2RouterServiceType))
            };
        }
    }
}
