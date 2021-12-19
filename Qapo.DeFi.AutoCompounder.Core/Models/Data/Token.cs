
namespace Qapo.DeFi.AutoCompounder.Core.Models.Data
{
    public class Token
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public string Address { get; set; }

        public int Decimals { get; set; }

        public int ChainId { get; set; }

        public string LogoUrl { get; set; }
    }
}
