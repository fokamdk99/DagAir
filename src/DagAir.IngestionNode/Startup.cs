using System;
using System.Threading.Tasks;
using DagAir.IngestionNode.Consumers;
using InfluxDB.Client.Api.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MassTransit;
using RabbitMQ.Client;

namespace DagAir.IngestionNode
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddMassTransit(x =>
            {
                x.AddConsumer<MeasurementsInsertedEventsConsumer>();
                x.AddConsumer<MeasurementsEventConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    
                });
            });
            
            services.AddMassTransitHostedService(true);
            
            services.AddHostedService<Worker>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
        }
    }
}