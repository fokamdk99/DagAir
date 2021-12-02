using System.Collections.Generic;
using DagAir.Components.HealthChecks;
using DagAir.Facilities.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

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

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:5001";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Facilities Policy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "DagAir.Facilities");
                });
            });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseConfiguredSwagger();
            
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                    .RequireAuthorization("Facilities Policy");
                endpoints.MapHealthCheckEndpoints();
            });
        }
    }
}