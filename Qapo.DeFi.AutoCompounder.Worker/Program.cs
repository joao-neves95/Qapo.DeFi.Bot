using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Autofac;

namespace Qapo.DeFi.AutoCompounder.Worker
{
    public static class Program
    {
        private static IContainer container;

        public static void Main(string[] args)
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            IoC.Configure(containerBuilder);
            Program.container = containerBuilder.Build();

            using (ILifetimeScope scope = Program.container.BeginLifetimeScope())
            {
                CreateHostBuilder(args).Build().Run();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host
                .CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) => services.AddHostedService<Worker>());
        }
    }
}
