using System.Collections.Generic;
using DagAir.Components.HealthChecks;
using DagAir.Facilities.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DagAir.Facilities
{
    public class Startup
    {
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
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseConfiguredSwagger();
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthCheckEndpoints();
            });
        }
    }
}