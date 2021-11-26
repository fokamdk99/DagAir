using DagAir.Addresses.Addresses.Queries;
using DagAir.Addresses.Contracts.Commands;
using DagAir.Addresses.Data.AppEntities;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Addresses.Addresses
{
    public static class AddressesEntitiesFeature
    {
        public static IServiceCollection AddAddressesEntitiesFeature(this IServiceCollection services)
        {
            services.AddScoped<IGetAddressQuery, GetAddressQuery>();
            services.AddScoped<ICommandHandler<AddNewAddressCommand, Address>>();
            
            return services;
        }
    }
}