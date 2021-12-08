
namespace Qapo.DeFi.AutoCompounder.Core.Models.Config
{
    public class AppConfig
    {
        public int WorkerMillisecondsDelay { get; set; }

        public BlockchainsConfig BlockchainsConfig  { get; set; }

        public AutoCompounderConfig AutoCompounderConfig { get; set; }
    }
}
