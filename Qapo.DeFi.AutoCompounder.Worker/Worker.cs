using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

using MediatR;

using Qapo.DeFi.AutoCompounder.Core.Interfaces.Services;
using Qapo.DeFi.AutoCompounder.Core.Interfaces.Stores;
using Qapo.DeFi.AutoCompounder.Core.Models.Config;
using Qapo.DeFi.AutoCompounder.Core.Models.Data;
using Qapo.DeFi.AutoCompounder.Core.Extensions;
using Qapo.DeFi.AutoCompounder.Core.Commands;

namespace Qapo.DeFi.AutoCompounder.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IConfigurationService<AppConfig> _configurationService;

        private readonly ILockedVaultsStore _lockedVaultsStore;

        private readonly ILoggerService _logger;

        private readonly IMediator _mediator;

        public Worker(
            IConfigurationService<AppConfig> configurationService,
            ILockedVaultsStore lockedVaultsStore,
            ILoggerService logger,
            IMediator mediator
        )
        {
            this._configurationService = configurationService.ThrowIfNull(nameof(IConfigurationService<AppConfig>));
            this._lockedVaultsStore = lockedVaultsStore.ThrowIfNull(nameof(ILockedVaultsStore));
            this._logger = logger.ThrowIfNull(nameof(ILoggerService));
            this._mediator = mediator.ThrowIfNull(nameof(IMediator));
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await this.ExecuteAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    this._logger.LogInformation($"------  Worker running at: {DateTimeOffset.Now} ------");

                    AppConfig appConfig = await this._configurationService
                        .GetConfig()
                        .ThrowIfNull($"{nameof(this._configurationService)}.GetConfig<{nameof(AppConfig)}>()")
                    ;

                    this._logger.LogInformation($"Executing DB storage update");

                    await this.HandleDbFileStoresUpdate(appConfig);

                    this._logger.LogInformation($"Executing all vaults");

                    await this.HandleVaultsExecution(appConfig);

                    this._logger.LogInformation($"------  End, sleeping. ------");
                    this._logger.LogInformation($"");

                    await Task.Delay(appConfig.WorkerMillisecondsDelay, stoppingToken).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogFatal(ex, $"{nameof(Worker)}: FATAL EXCEPTION ON THE WORKER.");

                throw;
            }
            finally
            {
                await this.BeforeEndApplication();
            }
        }

        private async Task HandleDbFileStoresUpdate(AppConfig appConfig)
        {
            await this._mediator.Send(
                new UpdateLocalDbFromDataFiles()
                {
                    AppConfig = appConfig
                }
            );
        }

        private async Task HandleVaultsExecution(AppConfig appConfig)
        {
            List<LockedVault> lockedVaults = await this._lockedVaultsStore.GetAll();

            for (int i = 0; i < lockedVaults.Count; ++i) {
                await this._mediator.Send(
                    new AutoCompoundStrategy()
                    {
                        AppConfig = appConfig,
                        LockedVault = lockedVaults[i]
                    }
                );
            }
        }

        private Task BeforeEndApplication()
        {
            Serilog.Log.CloseAndFlush();

            return Task.CompletedTask;
        }
    }
}
