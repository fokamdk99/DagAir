using AutoMapper;
using NUnit.Framework;

namespace DagAir.Addresses.Tests.Mappers
{
    public class AddressesMappersConfigurationTests
    {
        private MapperConfiguration _config;
        
        [SetUp]
        public void Setup()
        {
            _config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AddressesMappings>();
            });
        }

        [Test]
        public void MapperConfiguration_ShouldBeValid()
        {
            _config.AssertConfigurationIsValid();
        }
    }
}