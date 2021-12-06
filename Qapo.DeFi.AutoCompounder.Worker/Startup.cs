using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MediatR;

namespace Qapo.DeFi.AutoCompounder.Worker
{
    public class Startup
    {
        public static void Configure(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));

            services.AddHostedService<Worker>();
        }
    }
}
