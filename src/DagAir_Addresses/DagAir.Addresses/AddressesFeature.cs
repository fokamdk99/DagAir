using AutoMapper;
using DagAir.Addresses.Addresses;
using DagAir.Addresses.Contracts.DTOs;
using DagAir.Addresses.Data;
using DagAir.Addresses.Data.AppEntities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Addresses
{
    public static class AddressesFeature
    {
        public static IServiceCollection AddAddressesFeature(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDagAirAddressesAppDbContext();
            services.AddAddressesEntitiesFeature();
            services.AddAutoMapper(typeof(AddressesFeature).Assembly);
            return services;
        }

        public class AddressesMappings : Profile
        {
            public AddressesMappings()
            {
                CreateMap<Address, AddressDto>();
                CreateMap<City, CityDto>();
                CreateMap<Country, CountryDto>();
                CreateMap<PostalCode, PostalCodeDto>();
            }
        }
    }
}