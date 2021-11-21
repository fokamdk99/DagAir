using AutoMapper;
using DagAir.Policies.Contracts.DTOs;
using DagAir.Policies.Data.AppEntities;
using NUnit.Framework;

namespace DagAir.Policies.Tests.Mappers
{
    public class PoliciesMappersConfigurationTests
    {
        private MapperConfiguration _config;
        
        [SetUp]
        public void Setup()
        {
            _config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FooProfile>();
            });
        }

        [Test]
        public void Test1()
        {
            _config.AssertConfigurationIsValid();
        }
        
        internal class FooProfile : Profile
        {
            public FooProfile()
            {
                CreateMap<RoomPolicyDto, RoomPolicy>()
                    .ForMember(x => x.Created, opts => opts.Ignore())
                    .ForMember(x => x.Modified, opts => opts.Ignore());
                CreateMap<ExpectedRoomConditionsDto, ExpectedRoomConditions>()
                    .ForMember(x => x.Created, opts => opts.Ignore())
                    .ForMember(x => x.Modified, opts => opts.Ignore())
                    .ForMember(x => x.RoomPolicies, opts => opts.Ignore());
                CreateMap<RoomPolicyCategoryDto, RoomPolicyCategory>()
                    .ForMember(x => x.Created, opts => opts.Ignore())
                    .ForMember(x => x.Modified, opts => opts.Ignore());
            }
        }
    }
}