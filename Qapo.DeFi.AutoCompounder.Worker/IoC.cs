using System.Reflection;

using Autofac;
using MediatR;
using Serilog;
using Serilog.Core;

using Qapo.DeFi.AutoCompounder.Infrastructure.TypeFactory;
using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;
using Qapo.DeFi.AutoCompounder.Core.Commands;

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

            // TODO: Refactor. Move the configuration here.
            containerBuilder
                .RegisterInstance(TypeFac.GetInstance<Logger>(InfrastructureType.Serilog))
                .As<ILogger>()
                .SingleInstance();

            containerBuilder
                .RegisterType(TypeFac.GetType(InfrastructureType.SerilogLoggerService))
                .As<ILoggerService>()
                .InstancePerLifetimeScope();

            #endregion INFRASTRUCTURE SERVICES
        }
    }
}
