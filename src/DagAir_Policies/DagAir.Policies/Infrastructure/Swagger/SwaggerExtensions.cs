using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace DagAir.Policies.Infrastructure.Swagger
{
    public static class SwaggerExtensions
    {
        public static void UseConfiguredSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{PoliciesApiVersions.PoliciesV1}/swagger.json", "DagAir Policies Api");
            });
        }

        public static void AddConfiguredSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(PoliciesApiVersions.PoliciesV1, new OpenApiInfo(){Version = PoliciesApiVersions.V1, Title = $"DagAir Policies API {PoliciesApiVersions.V1}"});
            });
        }
    }
}