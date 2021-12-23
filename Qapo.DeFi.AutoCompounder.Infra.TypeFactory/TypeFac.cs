using System;

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
    }
}
