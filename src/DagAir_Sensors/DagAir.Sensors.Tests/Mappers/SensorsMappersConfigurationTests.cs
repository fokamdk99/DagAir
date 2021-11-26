using AutoMapper;
using NUnit.Framework;

namespace DagAir.Sensors.Tests.Mappers
{
    public class SensorsMappersConfigurationTests
    {
        private MapperConfiguration _config;
        
        [SetUp]
        public void Setup()
        {
            _config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SensorsMappings>();
            });
        }

        [Test]
        public void MapperConfiguration_ShouldBeValid()
        {
            _config.AssertConfigurationIsValid();
        }
    }
}