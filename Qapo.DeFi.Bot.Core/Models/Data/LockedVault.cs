
using Qapo.DeFi.Bot.Core.Interfaces.Dto;
using Qapo.DeFi.Bot.Core.Extensions;

namespace Qapo.DeFi.Bot.Core.Models.Data
{
    public class LockedVault : IEntity
    {
        public int? Id
        {
            get { return this.VaultAddress.IntRepresentation(); }

            set { }
        }

        public string VaultAddress { get; set; }

        public string Name { get; set; }

        public string UnderlyingAssetAddress { get; set; }

        public string RewardAssetAddress { get; set; }

        public string ChefAddress { get; set; }

        public int PoolId { get; set; }

        public int BlockchainId { get; set; }

        public int DexId { get; set; }

        public string LockedStratServiceType { get; set; }

        public int? StartBlock { get; set; }

        public long? StartTimestamp { get; set; }

        public float? MinGasPercentOffsetToExecute { get; set; }

        public int? MinSecondsBetweenExecutions { get; set; }

        public int? MaxSecondsBetweenExecutions { get; set; }

        public long? LastFarmedTimestamp { get; set; }
    }
}
