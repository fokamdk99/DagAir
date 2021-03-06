using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DagAir.Components.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DagAir.PolicyNode
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDagAirHealthChecks(new List<string>());
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