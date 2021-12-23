using System;

using Serilog;

using Qapo.DeFi.AutoCompounder.Infra.Services;
using Qapo.DeFi.AutoCompounder.Infra.Stores;

namespace Qapo.DeFi.AutoCompounder.Infra.TypeFactory
{
    public static class TypeFac
    {
        public static Type GetType(InfrastructureType infrastructureType)
        {
            return infrastructureType switch
            {
                InfrastructureType.LocalFileConfigurationService => typeof(LocalFileConfigurationService<>),
                InfrastructureType.SerilogLoggerService => typeof(SerilogLoggerService),

                InfrastructureType.BlockchainFileStore => typeof(BlockchainFileStore),
                InfrastructureType.DexFileStore => typeof(DexFileStore),
                InfrastructureType.TokenFileStore => typeof(TokenFileStore),
                InfrastructureType.LockedVaultsFileStore => typeof(LockedVaultsFileStore),

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
