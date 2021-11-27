using DagAir.Addresses.Addresses.Commands;
using DagAir.Addresses.Contracts.Commands;
using DagAir.Addresses.Countries.Queries;
using DagAir.Addresses.Data.AppEntities;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Addresses.Countries
{
    public static class CountriesEntitiesFeature
    {
        public static IServiceCollection AddCountriesEntitiesFeature(this IServiceCollection services)
        {
            services.AddScoped<IGetCountryQuery, GetCountryQuery>();
            services.AddScoped<ICommandHandler<AddNewCountryCommand, Country>, AddNewCountryCommandHandler>();

            return services;
        }
    }
}