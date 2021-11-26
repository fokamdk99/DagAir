using AutoMapper;
using NUnit.Framework;

namespace DagAir.Facilities.Tests.Mappers
{
    public class FacilitiesMappersConfigurationTests
    {
        private MapperConfiguration _config;
        
        [SetUp]
        public void Setup()
        {
            _config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FacilitiesMappings>();
            });
        }

        [Test]
        public void MapperConfiguration_ShouldBeValid()
        {
            _config.AssertConfigurationIsValid();
        }
    }
}