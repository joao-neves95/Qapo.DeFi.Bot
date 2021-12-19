
namespace Qapo.DeFi.AutoCompounder.Core.Models.Data
{
    public class LockedVault
    {
        public string VaultAddress { get; set; }

        public string Name { get; set; }

        public string UnderlyingAssetAddress { get; set; }

        public string RewardAssetAddress { get; set; }

        public int BlockchainId { get; set; }

        public int NativeAssetId { get; set; }

        public int DexId { get; set; }

        public string LockedStratServiceType { get; set; }

        public string UniswapV2RouterServiceType { get; set; }

        public int? StartBlock { get; set; }

        public long? StartTimestamp { get; set; }

        public float? MinGasPercentOffsetToExecute { get; set; }

        public int? SecondsOffsetBetweenExecutions { get; set; }

        public long? LastFarmedTimestamp { get; set; }
    }
}
