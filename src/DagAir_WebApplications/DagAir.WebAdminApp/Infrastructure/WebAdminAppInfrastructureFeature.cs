using DagAir.WebAdminApp.Affiliates;
using DagAir.WebAdminApp.Facilities;
using DagAir.WebAdminApp.Rooms;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.WebAdminApp.Infrastructure
{
    public static class WebAdminAppInfrastructureFeature
    {
        public static IServiceCollection AddWebAdminAppInfrastructureFeature(this IServiceCollection services)
        {
            services.AddScoped<IFacilitiesHandler, FacilitiesHandler>();
            services.AddScoped<IAffiliatesHandler, AffiliatesHandler>();
            services.AddScoped<IRoomsHandler, RoomsHandler>();
            services.AddSingleton<IExternalServices, ExternalServices>();

            return services;
        }
    }
}