
using Qapo.DeFi.Bot.Core.Interfaces.Dto;

namespace Qapo.DeFi.Bot.Core.Models.Data
{
    public class Dex : IEntity
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int ChainId { get; set; }

        public string UniswapV2RouterAddress { get; set; }

        public string UniswapV2RouterServiceType { get; set; }
    }
}
