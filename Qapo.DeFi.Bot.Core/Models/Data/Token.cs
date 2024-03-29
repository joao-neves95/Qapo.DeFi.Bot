
using Qapo.DeFi.Bot.Core.Interfaces.Dto;

namespace Qapo.DeFi.Bot.Core.Models.Data
{
    public class Token : IEntity
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public string Address { get; set; }

        public int Decimals { get; set; }

        public int ChainId { get; set; }

        public string LogoUrl { get; set; }
    }
}
