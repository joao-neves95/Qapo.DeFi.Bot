
using MediatR;
using Autofac;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;

namespace Qapo.DeFi.AutoCompounder.Worker
{
    public static class IoC
    {
        public static void Configure(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            // containerBuilder
            //     .RegisterType<ConsoleLogger>()
            //     .As<ICustomLoggerService>()
            //     .InstancePerLifetimeScope();
        }
    }
}
