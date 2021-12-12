using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace DagAir.AdminNode.Infrastructure.Swagger
{
    public static class SwaggerExtensions
    {
        public static void UseConfiguredSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{ApiVersions.AdminV1}/swagger.json", "DagAir Admin Node");
            });
        }

        public static void AddConfiguredSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ApiVersions.AdminV1, new OpenApiInfo(){Version = ApiVersions.V1, Title = $"DagAir Admin Node {ApiVersions.V1}"});
            });
        }
    }
}