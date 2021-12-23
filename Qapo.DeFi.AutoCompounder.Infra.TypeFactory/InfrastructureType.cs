namespace Qapo.DeFi.AutoCompounder.Infra.TypeFactory
{
    public enum InfrastructureType
    {
        LocalFileConfigurationService,
        SerilogLoggerService,

        BlockchainFileStore,
        DexFileStore,
        TokenFileStore,
        LockedVaultsFileStore
    }
}
