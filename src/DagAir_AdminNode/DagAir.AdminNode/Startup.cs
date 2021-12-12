using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DagAir.AdminNode.Hubs;
using DagAir.AdminNode.Infrastructure.Swagger;
using DagAir.Components.HealthChecks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DagAir.AdminNode
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddApiExplorer();
            services.AddControllers();
            services.AddSignalR();
            services.AddDagAirHealthChecks(new List<string>());
            services.AddCors();
            services.AddConfiguredSwagger();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseConfiguredSwagger();

            app.UseHttpsRedirection();
            app.UseRouting();
            
            app.UseCors(builder =>
            {
                builder.AllowCredentials();
                builder.WithOrigins(configuration.GetSection("serviceUrls:webAdminApp").Value);
                builder.AllowAnyHeader();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chatHub");
                endpoints.MapHealthCheckEndpoints();
            });
        }
    }
}