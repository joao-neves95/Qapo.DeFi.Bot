using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Qapo.DeFi.AutoCompounder.Core.Interfaces.Services
{
    public interface ICustomLoggerService
    {
        void LogInformation(string content);

        void LogWarning(string content);

        void LogError(string content);

        void LogDebug(string content);
    }
}
