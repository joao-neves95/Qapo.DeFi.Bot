
namespace Qapo.DeFi.AutoCompounder.Core.Models.Config
{
    public class AppConfig
    {
        public string LocalDataFilesPath { get; set; }

        public string LocalJsonDbFilesPath { get; set; }

        public int WorkerMillisecondsDelay { get; set; }

        public SecretsConfig SecretsConfig { get; set; }

        public BlockchainsConfig BlockchainsConfig  { get; set; }

        public AutoCompounderConfig AutoCompounderConfig { get; set; }
    }
}
