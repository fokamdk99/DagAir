using System;
using NUnit.Framework;
using AutoMapper;

namespace DagAir.Components.Tests
{
    public class MapperConfigurationTests
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
        
        [Test]
        public void Test2()
        {
            var mapper = new Mapper(_config);

            var mappingResult = mapper.Map<Foo, FooDto>(new Foo
            {
                Id = 1,
                EstablishmentDate = DateTime.Now,
                Foo2Object = new Foo2
                {
                    Id = 22,
                    Name = "jakis skomplikowany obiekt"
                }
            });
            
            Assert.Pass();
        }

        public class Foo
        {
            public int Id { get; set; }
            public string? WeekDay { get; set; }
            public DateTime EstablishmentDate { get; set; }
            public Foo2 Foo2Object { get; set; } 
        }

        public class Foo2
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class FooDto
        {
            public int Id { get; set; }
            public string? WeekDay { get; set; }
            public DateTime EstablishmentDate { get; set; }
            public Foo2Dto Foo2Object { get; set; }
        }
        
        public class Foo2Dto
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        
        internal class FooProfile : Profile
        {
            public FooProfile()
            {
                CreateMap<Foo, FooDto>();
                CreateMap<Foo2, Foo2Dto>();
            }
        }
    }
}