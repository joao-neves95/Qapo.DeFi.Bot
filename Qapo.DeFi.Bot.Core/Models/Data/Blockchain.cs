
using Qapo.DeFi.Bot.Core.Interfaces.Dto;

namespace Qapo.DeFi.Bot.Core.Models.Data
{
    public class Blockchain : IEntity
    {
        public int? Id
        {
            get { return this.ChainId; }

            set { this.ChainId = value ?? -1; }
        }

        public int ChainId { get; set; }

        public string Name { get; set; }

        public string RpcUrl { get; set; }

        public int NativeTokenId { get; set; }
    }
}
