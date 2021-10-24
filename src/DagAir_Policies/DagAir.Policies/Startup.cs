using DagAir.Components.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DagAir.Policies
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDagAirHealthChecks();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(HealthCheckExtensions.MapHealthCheckEndpoints);
        }
    }
}