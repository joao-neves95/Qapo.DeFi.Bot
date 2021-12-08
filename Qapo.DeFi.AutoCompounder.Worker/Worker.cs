using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;

namespace Qapo.DeFi.AutoCompounder.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILoggerService _logger;

        public Worker(ILoggerService logger)
        {
            this._logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                this._logger.LogInformation($"Worker running at: {DateTimeOffset.Now}");

                await Task.Delay(1000, stoppingToken).ConfigureAwait(false);
            }
        }
    }
}
