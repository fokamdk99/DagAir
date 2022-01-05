using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace DagAir.Addresses.Infrastructure.Swagger
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
                    c.SwaggerEndpoint($"/{basePath.Substring(1)}/swagger/{AddressesApiVersions.AddressesV1}/swagger.json", "DagAir Addresses Api");
                }
                c.SwaggerEndpoint($"/swagger/{AddressesApiVersions.AddressesV1}/swagger.json", "DagAir Addresses Api");
            });
        }

        public static void AddConfiguredSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(AddressesApiVersions.AddressesV1, new OpenApiInfo(){Version = AddressesApiVersions.V1, Title = $"DagAir Addresses API {AddressesApiVersions.V1}"});
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }
    }
}