using System;
using DagAir.IngestionNode.Consumers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MassTransit;

namespace DagAir.IngestionNode
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<MeasurementsInsertedEventsConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ReceiveEndpoint(new TemporaryEndpointDefinition($"dagair_ingestion_node_{new Guid()}"), e =>
                    {
                        e.ConfigureConsumer<MeasurementsInsertedEventsConsumer>(context);
                    });
                });
            });
        }
    }
}