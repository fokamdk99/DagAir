using AutoMapper;
using DagAir.Policies.Contracts.DTOs;
using DagAir.Policies.Data;
using DagAir.Policies.Data.AppEntities;
using DagAir.Policies.Policies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Policies
{
    public static class PoliciesFeature
    {
        public static IServiceCollection AddPoliciesFeature(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDagAirPoliciesAppDbContext();
            services.AddPolicyEntitiesFeature();
            services.AddAutoMapper(typeof(PoliciesFeature).Assembly);
            return services;
        }

        public class PoliciesMappings : Profile
        {
            public PoliciesMappings()
            {
                CreateMap<RoomPolicy, RoomPolicyDto>();
                CreateMap<RoomPolicyCategory, RoomPolicyCategoryDto>();
                CreateMap<ExpectedRoomConditions, ExpectedRoomConditionsDto>();
                CreateMap<RoomPolicyDto, RoomPolicy>()
                    .ForMember(x => x.Created, opts => opts.Ignore())
                    .ForMember(x => x.Modified, opts => opts.Ignore());
                CreateMap<ExpectedRoomConditionsDto, ExpectedRoomConditions>()
                    .ForMember(x => x.Created, opts => opts.Ignore())
                    .ForMember(x => x.Modified, opts => opts.Ignore());
                CreateMap<RoomPolicyCategoryDto, RoomPolicyCategory>()
                    .ForMember(x => x.Created, opts => opts.Ignore())
                    .ForMember(x => x.Modified, opts => opts.Ignore());
            }
        }
    }
}