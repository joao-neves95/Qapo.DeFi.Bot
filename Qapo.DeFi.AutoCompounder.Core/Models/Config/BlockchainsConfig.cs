using System;

using Qapo.DeFi.AutoCompounder.Core.Constants;

namespace Qapo.DeFi.AutoCompounder.Core.Models.Config
{
    public class BlockchainsConfig
    {
        public float PolygonDefaultGas { get; set; }

        public float PolygonMaxGas { get; set; }

        public float FantomDefaultGas { get; set; }

        public float FantomMaxGas { get; set; }

        public float BscDefaultGas { get; set; }

        public float BscMaxGas { get; set; }

        public float GetDefaultGasByChainId(int chainId)
        {
            return chainId switch
            {
                ChainId.Polygon => this.PolygonDefaultGas,
                ChainId.Fantom => this.FantomDefaultGas,

                _ => throw new ArgumentException($"[BlockchainsConfig.GetDefaultGasByChainId]: Unknown chainId - {chainId}.")
            };
        }

        public float GetMaxGasByChainId(int chainId)
        {
            return chainId switch
            {
                ChainId.Polygon => this.PolygonMaxGas,
                ChainId.Fantom => this.FantomMaxGas,

                _ => throw new ArgumentException($"[BlockchainsConfig.GetMaxGasByChainId]: Unknown chainId - {chainId}.")
            };
        }
    }
}
