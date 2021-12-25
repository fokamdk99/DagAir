using DagAir.AdminNode.Addresses;
using DagAir.AdminNode.Affiliates;
using DagAir.AdminNode.Facilities;
using DagAir.AdminNode.Rooms;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.AdminNode.Infrastructure
{
    public static class AdminNodeInfrastructureFeature
    {
        public static IServiceCollection AddAdminNodeInfrastructureFeature(this IServiceCollection services)
        {
            services.AddScoped<IFacilitiesHandler, FacilitiesHandler>();
            services.AddScoped<IAffiliatesHandler, AffiliatesHandler>();
            services.AddScoped<IAddressesHandler, AddressesHandler>();
            services.AddScoped<IRoomsHandler, RoomsHandler>();
            services.AddSingleton<IExternalServices, ExternalServices>();

            return services;
        }
    }
}