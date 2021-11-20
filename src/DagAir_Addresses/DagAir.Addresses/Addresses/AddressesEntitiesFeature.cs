using DagAir.Addresses.Addresses.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Addresses.Addresses
{
    public static class AddressesEntitiesFeature
    {
        public static IServiceCollection AddAddressesEntitiesFeature(this IServiceCollection services)
        {
            services.AddScoped<IGetAddressQuery, GetAddressQuery>();
            
            return services;
        }
    }
}