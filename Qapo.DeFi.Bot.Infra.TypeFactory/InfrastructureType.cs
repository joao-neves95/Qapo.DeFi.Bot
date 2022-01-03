namespace Qapo.DeFi.Bot.Infra.TypeFactory
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
