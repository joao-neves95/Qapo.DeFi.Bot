using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Qapo.DeFi.AutoCompounder.Core.Interfaces.Services
{
    public interface ICustomLoggerService
    {
        /// <summary>
        /// For trace debugging; begin method X, end method X.
        /// </summary>
        void LogTrace(string content);

        void LogTrace(Exception exception, string content);

        /// <summary>
        /// For debugging; executed query, user authenticated, session expired.
        /// </summary>
        void LogDebug(string content);

        void LogDebug(Exception exception, string content);

        /// <summary>
        /// Normal behavior like mail sent, user updated profile etc.
        /// </summary>
        void LogInformation(string content);

        void LogInformation(Exception exception, string content);

        /// <summary>
        /// Something unexpected; application will continue.
        /// Non-critical issues, which can be recovered or which are temporary failures.
        /// </summary>
        void LogWarning(string content);

        void LogWarning(Exception exception, string content);

        /// <summary>
        /// Something failed; application may or may not continue.
        /// </summary>
        void LogError(string content);

        void LogError(Exception exception, string content);

        /// <summary>
        /// Something bad happened; application is going down.
        /// </summary>
        void LogFatal(string content);

        void LogFatal(Exception exception, string content);
    }
}
