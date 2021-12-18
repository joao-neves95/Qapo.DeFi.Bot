using System;

using Nethereum.Web3;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Web3Services;
using Qapo.DeFi.AutoCompounder.Core.Web3Services.SushiSwapLpLockedStrat;

namespace Qapo.DeFi.AutoCompounder.Core.Factories
{
    public static class LockedStratServicesFactory
    {
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
