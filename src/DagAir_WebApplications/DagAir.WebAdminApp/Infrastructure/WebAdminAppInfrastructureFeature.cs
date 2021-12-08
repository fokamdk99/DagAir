using DagAir.WebAdminApp.Affiliates;
using DagAir.WebAdminApp.Facilities;
using DagAir.WebAdminApp.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.WebAdminApp.Infrastructure
{
    public static class WebAdminAppInfrastructureFeature
    {
        public static IServiceCollection AddWebAdminAppInfrastructureFeature(this IServiceCollection services)
        {
            services.AddScoped<IFacilitiesHandler, FacilitiesHandler>();
            services.AddScoped<IAffiliatesHandler, AffiliatesHandler>();
            services.AddSingleton<IExternalServices, ExternalServices>();
            services.AddScoped<IIdentityHandler, IdentityHandler>();
            
            return services;
        }
    }
}