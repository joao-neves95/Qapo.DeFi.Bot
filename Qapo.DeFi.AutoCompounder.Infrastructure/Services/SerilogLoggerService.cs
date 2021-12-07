using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Serilog;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;

namespace Qapo.DeFi.AutoCompounder.Infrastructure.Services
{
    public class SerilogLoggerService : ICustomLoggerService
    {
        private readonly ILogger logger;

        public SerilogLoggerService(ILogger logger)
        {
            this.logger = logger;
            Log.Logger = logger;
        }

        public void LogTrace(string content)
        {
            logger.Verbose(content);
        }

        public void LogTrace(Exception exception, string content)
        {
            logger.Verbose(exception, content);
        }

        public void LogDebug(string content)
        {
            logger.Debug(content);
        }

        public void LogDebug(Exception exception, string content)
        {
            logger.Debug(exception, content);
        }

        public void LogInformation(string content)
        {
            logger.Information(content);
        }

        public void LogInformation(Exception exception, string content)
        {
            logger.Information(exception, content);
        }

        public void LogWarning(string content)
        {
            logger.Warning(content);
        }

        public void LogWarning(Exception exception, string content)
        {
            logger.Warning(exception, content);
        }

        public void LogError(string content)
        {
            logger.Error(content);
        }

        public void LogError(Exception exception, string content)
        {
            logger.Error(exception, content);
        }

        public void LogFatal(string content)
        {
            logger.Fatal(content);
        }

        public void LogFatal(Exception exception, string content)
        {
            logger.Fatal(exception, content);
        }
    }
}
