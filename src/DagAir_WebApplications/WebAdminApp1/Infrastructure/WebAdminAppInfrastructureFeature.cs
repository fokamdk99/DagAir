using Microsoft.Extensions.DependencyInjection;
using WebAdminApp1.Affiliates;
using WebAdminApp1.Facilities;
using WebAdminApp1.Rooms;

namespace WebAdminApp1.Infrastructure
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