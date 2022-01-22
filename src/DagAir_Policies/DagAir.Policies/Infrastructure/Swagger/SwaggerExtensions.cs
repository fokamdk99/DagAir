using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace DagAir.Policies.Infrastructure.Swagger
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
                    c.SwaggerEndpoint($"/{basePath.Substring(1)}/swagger/{PoliciesApiVersions.PoliciesV1}/swagger.json", "DagAir Policies Api");
                }
                c.SwaggerEndpoint($"/swagger/{PoliciesApiVersions.PoliciesV1}/swagger.json", "DagAir Policies Api");
            });
        }

        public static void AddConfiguredSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(PoliciesApiVersions.PoliciesV1, new OpenApiInfo(){Version = PoliciesApiVersions.V1, Title = $"DagAir Policies API {PoliciesApiVersions.V1}"});
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }
    }
}