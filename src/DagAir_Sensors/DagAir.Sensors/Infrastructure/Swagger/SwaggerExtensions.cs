using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace DagAir.Sensors.Infrastructure.Swagger
{
    public static class SwaggerExtensions
    {
        private const string BasePathSection = "basePath";
        public static void UseConfiguredSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            var basePath = configuration.GetSection(BasePathSection).Value;
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                if (basePath != null)
                {
                    c.SwaggerEndpoint($"/{basePath.Substring(1)}/swagger/{SensorsApiVersions.SensorsV1}/swagger.json", "DagAir Sensors Api");
                }
                c.SwaggerEndpoint($"/swagger/{SensorsApiVersions.SensorsV1}/swagger.json", "DagAir Sensors Api");
            });
        }

        public static void AddConfiguredSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(SensorsApiVersions.SensorsV1, new OpenApiInfo(){Version = SensorsApiVersions.V1, Title = $"DagAir Sensors API {SensorsApiVersions.V1}"});
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }
    }
}