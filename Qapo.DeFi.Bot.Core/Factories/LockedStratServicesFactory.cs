using System;

using Nethereum.Web3;

using Qapo.DeFi.Bot.Core.Interfaces.Web3Services;
using Qapo.DeFi.Bot.Core.Web3Services.SushiSwapLpLockedStrat;

namespace Qapo.DeFi.Bot.Core.Factories
{
    public static class LockedStratServicesFactory
    {
        public static ILockedStratService Get(string web3ServiceTypeStr, Web3 web3, string contractAddress)
        {
            return LockedStratServicesFactory.Get(
                (LockedStratServiceType)Enum.Parse(typeof(LockedStratServiceType), web3ServiceTypeStr),
                web3,
                contractAddress
            );
        }

        public static ILockedStratService Get(LockedStratServiceType web3ServiceType, Web3 web3, string contractAddress)
        {
            return web3ServiceType switch
            {
                LockedStratServiceType.SushiSwapLpLockedStratService => new SushiSwapLpLockedStratService(web3, contractAddress),

                _ => throw new TypeAccessException("Unknown type: " + Enum.GetName(typeof(LockedStratServiceType), web3ServiceType))
            };
        }
    }
}
