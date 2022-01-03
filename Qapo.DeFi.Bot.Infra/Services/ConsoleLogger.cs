using System;

using Qapo.DeFi.Bot.Core.Interfaces.Services;

namespace Qapo.DeFi.Bot.Infra.Services
{
    public class ConsoleLogger : ILoggerService
    {
        public void LogDebug(string content)
        {
            Console.WriteLine($"DEBUG: {content}");
        }

        public void LogDebug(Exception exception, string content)
        {
            Console.WriteLine($"DEBUG: {content};\nException: {exception}");
        }

        public void LogError(string content)
        {
            Console.WriteLine($"ERROR: {content}");
        }

        public void LogError(Exception exception, string content)
        {
            Console.WriteLine($"ERROR: {content};\nException: {exception}");
        }

        public void LogFatal(string content)
        {
            Console.WriteLine($"FATAL: {content}");
        }

        public void LogFatal(Exception exception, string content)
        {
            Console.WriteLine($"FATAL: {content};\nException: {exception}");
        }

        public void LogInformation(string content)
        {
            Console.WriteLine($"INFO: {content}");
        }

        public void LogInformation(Exception exception, string content)
        {
            Console.WriteLine($"INFO: {content};\nException: {exception}");
        }

        public void LogTrace(string content)
        {
            Console.WriteLine($"TRACE: {content}");
        }

        public void LogTrace(Exception exception, string content)
        {
            Console.WriteLine($"TRACE: {content};\nException: {exception}");
        }

        public void LogWarning(string content)
        {
            Console.WriteLine($"WARN: {content}");
        }

        public void LogWarning(Exception exception, string content)
        {
            Console.WriteLine($"WARN: {content};\nException: {exception}");
        }
    }
}
