using AutoMapper;
using DagAir.Addresses.Addresses;
using DagAir.Addresses.Cities;
using DagAir.Addresses.Contracts.DTOs;
using DagAir.Addresses.Countries;
using DagAir.Addresses.Data;
using DagAir.Addresses.Data.AppEntities;
using DagAir.Addresses.PostalCodes;
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
            services.AddCitiesEntitiesFeature();
            services.AddCountriesEntitiesFeature();
            services.AddPostalCodesEntitiesFeature();
            services.AddAutoMapper(typeof(AddressesFeature).Assembly);
            return services;
        }
    }
    
    public class AddressesMappings : Profile
    {
        public AddressesMappings()
        {
            CreateMap<Address, AddressDto>();
            CreateMap<City, CityDto>();
            CreateMap<Country, CountryDto>();
            CreateMap<PostalCode, PostalCodeDto>();
            
            CreateMap<AddressDto, Address>()
                .ForMember(x => x.Created, opts => opts.Ignore())
                .ForMember(x => x.Modified, opts => opts.Ignore())
                .ForMember(x => x.Country, opts => opts.Ignore())
                .ForMember(x => x.City, opts => opts.Ignore())
                .ForMember(x => x.PostalCode, opts => opts.Ignore());
            CreateMap<CityDto, City>()
                .ForMember(x => x.Created, opts => opts.Ignore())
                .ForMember(x => x.Modified, opts => opts.Ignore())
                .ForMember(x => x.Addresses, opts => opts.Ignore());
            CreateMap<CountryDto, Country>()
                .ForMember(x => x.Created, opts => opts.Ignore())
                .ForMember(x => x.Modified, opts => opts.Ignore())
                .ForMember(x => x.Addresses, opts => opts.Ignore());
            CreateMap<PostalCodeDto, PostalCode>()
                .ForMember(x => x.Created, opts => opts.Ignore())
                .ForMember(x => x.Modified, opts => opts.Ignore())
                .ForMember(x => x.Addresses, opts => opts.Ignore());
        }
    }
}