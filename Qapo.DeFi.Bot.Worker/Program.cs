using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Autofac;

namespace Qapo.DeFi.Bot.Worker
{
    public static class Program
    {
        private static IContainer container;

        public static async Task Main(string[] args)
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            IocConfig.Configure(containerBuilder);
            Program.container = containerBuilder.Build();

            using (ILifetimeScope scope = Program.container.BeginLifetimeScope())
            {
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                Worker worker = scope.Resolve<Worker>();

                await worker.StartAsync(cancellationTokenSource.Token);
                worker.Dispose();
            }
        }
    }
}
