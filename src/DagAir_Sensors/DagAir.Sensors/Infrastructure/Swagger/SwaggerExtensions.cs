using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace DagAir.Sensors.Infrastructure.Swagger
{
    public static class SwaggerExtensions
    {
        public static void UseConfiguredSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{SensorsApiVersions.SensorsV1}/swagger.json", "DagAir Sensors Api");
            });
        }

        public static void AddConfiguredSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(SensorsApiVersions.SensorsV1, new OpenApiInfo(){Version = SensorsApiVersions.V1, Title = $"DagAir Sensors API {SensorsApiVersions.V1}"});
            });
        }
    }
}