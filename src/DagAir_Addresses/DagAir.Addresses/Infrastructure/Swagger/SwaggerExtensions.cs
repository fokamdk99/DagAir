using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace DagAir.Addresses.Infrastructure.Swagger
{
    public static class SwaggerExtensions
    {
        public static void UseConfiguredSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{ApiVersions.AddressesV1}/swagger.json", "DagAir Addresses Api");
            });
        }

        public static void AddConfiguredSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ApiVersions.AddressesV1, new OpenApiInfo(){Version = ApiVersions.V1, Title = $"DagAir Addresses API {ApiVersions.V1}"});
            });
        }
    }
}