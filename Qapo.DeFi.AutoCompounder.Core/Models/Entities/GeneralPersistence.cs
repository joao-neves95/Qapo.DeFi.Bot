
using Qapo.DeFi.AutoCompounder.Core.Interfaces.Dto;

namespace Qapo.DeFi.AutoCompounder.Core.Models.Entities
{
    public class GeneralPersistence : IEntity
    {
        public int? Id { get { return 1; } set {  } }

        public long LastDbUpdateTimestamp { get; set; }
    }
}
