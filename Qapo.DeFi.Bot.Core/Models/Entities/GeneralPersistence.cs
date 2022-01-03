
using Qapo.DeFi.Bot.Core.Interfaces.Dto;

namespace Qapo.DeFi.Bot.Core.Models.Entities
{
    public class GeneralPersistence : IEntity
    {
        public int? Id { get { return 1; } set {  } }

        public long LastDbUpdateTimestamp { get; set; }
    }
}
