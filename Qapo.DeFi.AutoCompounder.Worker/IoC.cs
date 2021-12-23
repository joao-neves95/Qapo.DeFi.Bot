using System.Reflection;

using Autofac;
using MediatR;
using Serilog;
using Serilog.Core;

using Qapo.DeFi.AutoCompounder.Infra.TypeFactory;
using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;
using Qapo.DeFi.AutoCompounder.Core.Commands;
using Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores;

namespace Qapo.DeFi.AutoCompounder.Worker
{
    public static class IoC
    {
        public static void Configure(ContainerBuilder containerBuilder)
        {
            #region MediatR

            // Mediator itself.
            containerBuilder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            containerBuilder
                .Register<ServiceFactory>(context =>
                {
                    var componentContext = context.Resolve<IComponentContext>();
                    return type => componentContext.Resolve(type);
                });

            containerBuilder
                .RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            // Command classes.
            containerBuilder
                .RegisterAssemblyTypes(typeof(AutoCompoundStrategyHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            #endregion MediatR

            #region CORE SERVICES

            #endregion CORE SERVICES

            #region INFRASTRUCTURE SERVICES

            containerBuilder
                .RegisterGeneric(TypeFac.GetType(InfrastructureType.LocalFileConfigurationService))
                .As(typeof(IConfigurationService<>))
                .InstancePerDependency()
            ;

            // TODO: Refactor. Move the configuration here.
            containerBuilder
                .RegisterInstance(TypeFac.GetInstance<Logger>(InfrastructureType.Serilog))
                .As<ILogger>()
                .SingleInstance();

            containerBuilder
                .RegisterType(TypeFac.GetType(InfrastructureType.SerilogLoggerService))
                .As<ILoggerService>()
                .InstancePerLifetimeScope();

            containerBuilder
                .RegisterType(TypeFac.GetType(InfrastructureType.BlockchainFileStore))
                .As<IBlockchainStore>()
                .InstancePerDependency()
            ;

            containerBuilder
                .RegisterType(TypeFac.GetType(InfrastructureType.DexFileStore))
                .As<IDexStore>()
                .InstancePerDependency()
            ;

            containerBuilder
                .RegisterType(TypeFac.GetType(InfrastructureType.LockedVaultsFileStore))
                .As<ILockedVaultsStore>()
                .InstancePerDependency()
            ;

            #endregion INFRASTRUCTURE SERVICES
        }
    }
}
