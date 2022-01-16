namespace Qapo.DeFi.Bot.Infra.TypeFactory
{
    public enum InfrastructureType
    {
        LocalFileConfigurationService,
        SerilogLoggerService,
        WebScraperGasPriceService,

        GeneralPersistenceFileStore,
        BlockchainFileStore,
        DexFileStore,
        TokenFileStore,
        LockedVaultsFileStore
    }
}
