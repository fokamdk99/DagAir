using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DagAir.Components.HealthChecks;
using DagAir.DataServices.SensorState.Infrastructure.Configuration;
using DagAir.DataServices.SensorState.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace DagAir.DataServices.SensorState
{
    public class Startup
    {
        private const string BasePathSection = "basePath";
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddApiExplorer();
            services.AddControllers();
            services.AddConfiguredSwagger();
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            var basePath = configuration.GetSection(BasePathSection).Value; 
            if (basePath != null)
            {
                app.UsePathBase(basePath);
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseConfiguredSwagger(configuration);
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthCheckEndpoints();
            });
        }
    }
}