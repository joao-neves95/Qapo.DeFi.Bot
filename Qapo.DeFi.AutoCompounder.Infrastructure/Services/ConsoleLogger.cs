using System;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;

namespace Qapo.DeFi.AutoCompounder.Infrastructure.Services
{
    public class ConsoleLogger : ICustomLoggerService
    {
        public void LogDebug(string content)
        {
            Console.WriteLine($"DEBUG: {content}");
        }

        public void LogError(string content)
        {
            Console.WriteLine($"ERROR: {content}");
        }

        public void LogInformation(string content)
        {
            Console.WriteLine($"INFO: {content}");
        }

        public void LogWarning(string content)
        {
            Console.WriteLine($"WARN: {content}");
        }
    }
}
