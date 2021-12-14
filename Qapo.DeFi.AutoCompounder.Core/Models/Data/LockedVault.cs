
namespace Qapo.DeFi.AutoCompounder.Core.Models.Data
{
    public class LockedVault
    {
        public string VaultAddress { get; set; }

        public string UnderlyingAssetAddress { get; set; }

        public string RewardAssetAddress { get; set; }

        public int BlockchainId { get; set; }

        public int? StartBlock { get; set; }

        public int? StartTimestamp { get; set; }

        public float? MinGasPercentOffsetToExecute { get; set; }

        public int? TimestampOffsetBetweenExecutions { get; set; }

        public int? LastFarmedTimestamp { get; set; }
    }
}
