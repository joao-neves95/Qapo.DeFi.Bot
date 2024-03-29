
using Qapo.DeFi.Bot.Core.Interfaces.Dto;

namespace Qapo.DeFi.Bot.Core.Models.Config
{
    public class AppConfig : IAppConfig
    {
        public string LocalDataFilesPath { get; set; }

        public string LocalJsonDbFilesPath { get; set; }

        public int WorkerMillisecondsDelay { get; set; }

        public int SecondsBetweenDbUpdate { get; set; }

        public SecretsConfig SecretsConfig { get; set; }

        public BlockchainsConfig BlockchainsConfig  { get; set; }

        public AutoCompounderConfig AutoCompounderConfig { get; set; }
    }
}
