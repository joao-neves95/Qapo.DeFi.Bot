using System.Reflection;

using Autofac;
using MediatR;
using Serilog;

using Qapo.DeFi.AutoCompounder.Infra.TypeFactory;
using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;
using Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores;
using Qapo.DeFi.AutoCompounder.Core.Commands;

namespace Qapo.DeFi.AutoCompounder.Worker
{
    public static class IocConfig
    {
        public static void Configure(ContainerBuilder containerBuilder)
        {
            #region MediatR

            // Mediator itself.
            containerBuilder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope()
            ;

            containerBuilder
                .Register<ServiceFactory>(context =>
                {
                    var componentContext = context.Resolve<IComponentContext>();
                    return type => componentContext.Resolve(type);
                })
            ;

            containerBuilder
                .RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces()
            ;

            // Command classes.
            containerBuilder
                .RegisterAssemblyTypes(typeof(AutoCompoundStrategyHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
            ;

            #endregion MediatR

            #region INFRASTRUCTURE SERVICES

            containerBuilder
                .RegisterGeneric(TypeFac.GetType(InfrastructureType.LocalFileConfigurationService))
                .As(typeof(IConfigurationService<>))
                .InstancePerDependency()
            ;

            containerBuilder
                .RegisterInstance(new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .WriteTo.Async(config => config.Console())
                    .WriteTo.Async(config => config.File(
                        "_data/logs/log_.txt",
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true
                    ))
                    .CreateLogger()
                )
                .As<ILogger>()
                .SingleInstance()
            ;

            containerBuilder
                .RegisterType(TypeFac.GetType(InfrastructureType.SerilogLoggerService))
                .As<ILoggerService>()
                .InstancePerLifetimeScope()
            ;

            containerBuilder
                .RegisterType(TypeFac.GetType(InfrastructureType.GeneralPersistenceFileStore))
                .As<IGeneralPersistenceStore>()
                .InstancePerLifetimeScope()
                // .InstancePerDependency()
            ;

            containerBuilder
                .RegisterType(TypeFac.GetType(InfrastructureType.BlockchainFileStore))
                .As<IBlockchainStore>()
                .InstancePerLifetimeScope()
                // .InstancePerDependency()
            ;

            containerBuilder
                .RegisterType(TypeFac.GetType(InfrastructureType.TokenFileStore))
                .As<ITokenStore>()
                .InstancePerLifetimeScope()
                // .InstancePerDependency()
            ;

            containerBuilder
                .RegisterType(TypeFac.GetType(InfrastructureType.DexFileStore))
                .As<IDexStore>()
                .InstancePerLifetimeScope()
                // .InstancePerDependency()
            ;

            containerBuilder
                .RegisterType(TypeFac.GetType(InfrastructureType.LockedVaultsFileStore))
                .As<ILockedVaultsStore>()
                .InstancePerLifetimeScope()
                // .InstancePerDependency()
            ;

            #endregion INFRASTRUCTURE SERVICES

            #region CORE SERVICES

            #endregion CORE SERVICES

            containerBuilder
                .RegisterType<Worker>()
                .AsSelf()
                .InstancePerLifetimeScope()
            ;
        }
    }
}
