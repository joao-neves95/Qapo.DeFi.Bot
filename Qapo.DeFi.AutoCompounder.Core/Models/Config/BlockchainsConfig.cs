
namespace Qapo.DeFi.AutoCompounder.Core.Models.Config
{
    public class BlockchainsConfig
    {
        public string PolygonRpcEndpoint { get; set; }

        public int PolygonChainId { get; set; }

        public float PolygonDefaultGas { get; set; }

        public float PolygonMaxGas { get; set; }

        public string FantomRpcEndpoint { get; set; }

        public int FantomChainId { get; set; }

        public float FantomDefaultGas { get; set; }

        public float FantomMaxGas { get; set; }

        public string BscRpcEndpoint { get; set; }

        public int BscChainId { get; set; }

        public float BscDefaultGas { get; set; }

        public float BscMaxGas { get; set; }
    }
}
