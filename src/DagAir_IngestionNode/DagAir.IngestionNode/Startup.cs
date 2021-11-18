using System;
using System.Collections.Generic;
using DagAir.Components.HealthChecks;
using DagAir.IngestionNode.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
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
            services.AddDagAirHealthChecks(new List<string>());
            services
                .AddSingleton<IConnection>(sp=>
                {
                    var rabbitMqConfiguration = sp.GetRequiredService<ISensorRabbitMqConfiguration>();
                    var path = "amqp://" + rabbitMqConfiguration.UserName + ":" + rabbitMqConfiguration.Password + "@" + rabbitMqConfiguration.HostName + "/" +
                               rabbitMqConfiguration.VirtualHost;
                    var factory = new ConnectionFactory()
                    {
                        
                        Uri = new Uri(path),
                        AutomaticRecoveryEnabled = true
                    };

                    return  factory.CreateConnection();
                })
                .AddHealthChecks()
                .AddRabbitMQ(name: "sensorRabbitMq");

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(HealthCheckExtensions.MapHealthCheckEndpoints);
        }
    }
}