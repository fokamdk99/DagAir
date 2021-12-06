using DagAir.WebApps.WebAdminApp2.Facilities;
using DagAir.WebApps.WebAdminApp2.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.WebApps.WebAdminApp2.Infrastructure
{
    public static class WebAdminAppInfrastructureFeature
    {
        public static IServiceCollection AddWebAdminAppInfrastructureFeature(this IServiceCollection services)
        {
            services.AddScoped<IFacilitiesHandler, FacilitiesHandler>();
            services.AddSingleton<IExternalServices, ExternalServices>();
            services.AddScoped<IIdentityHandler, IdentityHandler>();
            
            return services;
        }
    }
}