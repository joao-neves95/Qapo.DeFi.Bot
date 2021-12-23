namespace Qapo.DeFi.AutoCompounder.Infra.TypeFactory
{
    public enum InfrastructureType
    {
        LocalFileConfigurationService,
        Serilog,
        SerilogLoggerService,

        BlockchainFileStore,
        DexFileStore,
        TokenFileStore,
        LockedVaultsFileStore
    }
}
