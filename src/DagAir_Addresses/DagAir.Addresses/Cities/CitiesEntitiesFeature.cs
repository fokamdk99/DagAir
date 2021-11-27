using DagAir.Addresses.Addresses.Commands;
using DagAir.Addresses.Cities.Queries;
using DagAir.Addresses.Contracts.Commands;
using DagAir.Addresses.Data.AppEntities;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Addresses.Cities
{
    public static class CitiesEntitiesFeature
    {
        public static IServiceCollection AddCitiesEntitiesFeature(this IServiceCollection services)
        {
            services.AddScoped<IGetCityQuery, GetCityQuery>();
            services.AddScoped<ICommandHandler<AddNewCityCommand, City>, AddNewCityCommandHandler>();
            
            return services;
        }
    }
}