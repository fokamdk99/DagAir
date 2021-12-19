using AutoMapper;
using DagAir.Facilities.Affiliates;
using DagAir.Facilities.Contracts.DTOs;
using DagAir.Facilities.Data;
using DagAir.Facilities.Data.AppEntitities;
using DagAir.Facilities.Organizations;
using DagAir.Facilities.Rooms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Facilities
{
    public static class FacilitiesFeature
    {
        public static IServiceCollection AddFacilitiesFeature(this IServiceCollection services,
            IConfiguration collection)
        {
            services.AddAffiliatesEntitiesFeature();
            services.AddOrganizationsEntitiesFeature();
            services.AddRoomsEntitiesFeature();
            services.AddDagAirFacilitiesDbContext();
            services.AddAutoMapper(typeof(FacilitiesFeature).Assembly);
            
            return services;
        }
    }
    
    public class FacilitiesMappings : Profile
    {
        public FacilitiesMappings()
        {
            CreateMap<Affiliate, AffiliateDto>();
            CreateMap<Organization, OrganizationDto>();
            CreateMap<Room, RoomDto>();

            CreateMap<AffiliateDto, Affiliate>()
                .ForMember(x => x.Created, opts => opts.Ignore())
                .ForMember(x => x.Modified, opts => opts.Ignore())
                .ForMember(x => x.Organization, opts => opts.Ignore())
                .ForMember(x => x.Rooms, opts => opts.Ignore());
            CreateMap<OrganizationDto, Organization>()
                .ForMember(x => x.Created, opts => opts.Ignore())
                .ForMember(x => x.Modified, opts => opts.Ignore())
                .ForMember(x => x.Affiliates, opts => opts.Ignore());
            CreateMap<RoomDto, Room>()
                .ForMember(x => x.Created, opts => opts.Ignore())
                .ForMember(x => x.Modified, opts => opts.Ignore())
                .ForMember(x => x.Affiliate, opts => opts.Ignore());
        }
    }
}