
namespace Qapo.DeFi.Bot.Core.Models.Config
{
    public class AutoCompounderConfig
    {
        public float DefaultMaxSecondsBetweenExecutions { get; set; }

        /// <summary>
        /// E.g.: 0.1 = 10%
        ///
        /// </summary>
        public float DefaultMinProfitToGasPercentOffset { get; set; }
    }
}
