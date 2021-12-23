using System;

using Serilog;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;

namespace Qapo.DeFi.AutoCompounder.Infra.Services
{
    public class SerilogLoggerService : ILoggerService
    {
        private readonly ILogger logger;

        public SerilogLoggerService(ILogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(SerilogLoggerService));
            Log.Logger = logger;
        }

        public void LogTrace(string content)
        {
            logger.Verbose($"{DateTimeOffset.Now} - {content}");
        }

        public void LogTrace(Exception exception, string content)
        {
            logger.Verbose(exception, $"{DateTimeOffset.Now} - {content}");
        }

        public void LogDebug(string content)
        {
            logger.Debug($"{DateTimeOffset.Now} - {content}");
        }

        public void LogDebug(Exception exception, string content)
        {
            logger.Debug(exception, $"{DateTimeOffset.Now} - {content}");
        }

        public void LogInformation(string content)
        {
            logger.Information($"{DateTimeOffset.Now} - {content}");
        }

        public void LogInformation(Exception exception, string content)
        {
            logger.Information(exception, $"{DateTimeOffset.Now} - {content}");
        }

        public void LogWarning(string content)
        {
            logger.Warning($"{DateTimeOffset.Now} - {content}");
        }

        public void LogWarning(Exception exception, string content)
        {
            logger.Warning(exception, $"{DateTimeOffset.Now} - {content}");
        }

        public void LogError(string content)
        {
            logger.Error($"{DateTimeOffset.Now} - {content}");
        }

        public void LogError(Exception exception, string content)
        {
            logger.Error(exception, $"{DateTimeOffset.Now} - {content}");
        }

        public void LogFatal(string content)
        {
            logger.Fatal($"{DateTimeOffset.Now} - {content}");
        }

        public void LogFatal(Exception exception, string content)
        {
            logger.Fatal(exception, $"{DateTimeOffset.Now} - {content}");
        }
    }
}
