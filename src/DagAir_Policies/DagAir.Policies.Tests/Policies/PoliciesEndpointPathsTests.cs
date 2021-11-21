using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DagAir.Policies.Data.AppEntities;
using FluentAssertions;
using NUnit.Framework;

namespace DagAir.Policies.Tests.Policies
{
    public class PoliciesEndpointPathsTests : IntegrationTestServer
    {
        private HttpClient _client;

        [Test]
        public async Task GetCurrentRoomPolicy_ShouldBeAvailableUnderDefinedPath()
        {
            var path = $"policies-api/policies/1";

            var response = await _client.GetAsync(path);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Test]
        public async Task GetCurrentRoomPolicy_WhenNoPolicyFound_ShouldReturnNotFound()
        {
            var path = $"policies-api/policies/2";

            var response = await _client.GetAsync(path);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        public override async Task Setup()
        {
            await AddDataToTheDatabase();
        }

        public override async Task OneTimeSetup()
        {
            await base.OneTimeSetup();

            _client = PoliciesApiTestClient.GetTestClient(TestServer, Configuration);
        }

        private async Task AddDataToTheDatabase()
        {
            await AppContext.Database.EnsureDeletedAsync();
            await AddExpectedRoomConditionsToTheDatabase();
            await AddRoomPolicyCategoriesToTheDatabase();
            await AddPoliciesToTheDatabase();
            
            await AppContext.SaveChangesAsync();
        }

        private async Task AddPoliciesToTheDatabase()
        {
            var policy = new RoomPolicy()
            {
                Id = 1,
                StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.AddHours(-3).Hour,
                    DateTime.Now.Minute, DateTime.Now.Second),
                EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                    DateTime.Now.AddHours(3).Hour, DateTime.Now.Minute, DateTime.Now.Second),
                RepeatOn = "",
                ExpectedConditionsId = 1L,
                CategoryId = 1,
                RoomId = 1
            };

            await AppContext.RoomPolicies.AddAsync(policy);
        }
        
        public async Task AddRoomPolicyCategoriesToTheDatabase()
        {
            var roomPolicyCategory = new RoomPolicyCategory()
            {
                Id = 1,
                Name = "Default",
                CategoryNumber = 0
            };

            await AppContext.RoomPolicyCategories.AddAsync(roomPolicyCategory);
        }
        
        public async Task AddExpectedRoomConditionsToTheDatabase()
        {
            var expectedRoomConditions = new ExpectedRoomConditions
            {
                Id = 1,
                Temperature = 20,
                Illuminance = 100,
                Humidity = 0.4f,
                TemperatureMargin = 2,
                IlluminanceMargin = 20,
                HumidityMargin = 0.1f
            };

            await AppContext.ExpectedRoomConditions.AddAsync(expectedRoomConditions);
        }
        
        
    }
}