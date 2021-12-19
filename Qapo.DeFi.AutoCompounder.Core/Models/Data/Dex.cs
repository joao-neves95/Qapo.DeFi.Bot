
namespace Qapo.DeFi.AutoCompounder.Core.Models.Data
{
    public class Dex
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ChainId { get; set; }

        public string UniswapV2RouterAddress { get; set; }

        public string UniswapV2RouterServiceType { get; set; }
    }
}
