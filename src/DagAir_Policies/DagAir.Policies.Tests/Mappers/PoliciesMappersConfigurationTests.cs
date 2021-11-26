using AutoMapper;
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
                cfg.AddProfile<PoliciesMappings>();
            });
        }

        [Test]
        public void MapperConfiguration_ShouldBeValid()
        {
            _config.AssertConfigurationIsValid();
        }
    }
}