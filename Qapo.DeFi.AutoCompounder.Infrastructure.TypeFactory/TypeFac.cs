using System;

using Serilog;

using Qapo.DeFi.AutoCompounder.Infrastructure.Services;

namespace Qapo.DeFi.AutoCompounder.Infrastructure.TypeFactory
{
    public static class TypeFac
    {
        public static Type GetType(InfrastructureType infrastructureType)
        {
            return infrastructureType switch
            {
                InfrastructureType.SerilogLoggerService => typeof(SerilogLoggerService),

                _ => throw new TypeAccessException("Unknown type: " + Enum.GetName(typeof(InfrastructureType), infrastructureType))
            };
        }

        public static T GetInstance<T>(InfrastructureType infrastructureType) where T : class
        {
            return infrastructureType switch
            {
                InfrastructureType.Serilog => new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .WriteTo.Console()
                    .WriteTo.File(
                        "log.txt",
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true
                    )
                    .CreateLogger() as T,

                _ => throw new TypeAccessException("Unknown type: " + Enum.GetName(typeof(InfrastructureType), infrastructureType))
            };
        }
    }
}
