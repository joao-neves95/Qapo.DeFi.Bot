namespace Qapo.DeFi.AutoCompounder.Infra.TypeFactory
{
    public enum InfrastructureType
    {
        LocalFileConfigurationService,
        SerilogLoggerService,

        GeneralPersistenceFileStore,
        BlockchainFileStore,
        DexFileStore,
        TokenFileStore,
        LockedVaultsFileStore
    }
}
