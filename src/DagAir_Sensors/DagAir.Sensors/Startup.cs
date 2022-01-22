using System.Collections.Generic;
using DagAir.Components.HealthChecks;
using DagAir.Sensors.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DagAir.Sensors
{
    public class Startup
    {
        private const string BasePathSection = "basePath";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddApiExplorer();
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
            services.AddConfiguredSwagger();
            var healthChecksToBeDisabled = new List<string>();
            healthChecksToBeDisabled.Add(HealthCheckFeature.RabbitMqHealthCheck);
            services.AddDagAirHealthChecks(healthChecksToBeDisabled);
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