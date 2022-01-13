using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DagAir.Policies.Data.AppEntities;
using FluentAssertions;
using NUnit.Framework;

namespace DagAir.Policies.Tests.RoomConditions
{
    public class ExpectedRoomConditionsEndpointPathsTests : IntegrationTestServer
    {
        private HttpClient _client;

        [Test]
        public async Task GetExpectedRoomConditions_ShouldBeAvailableUnderDefinedPath()
        {
            var path = $"policies-api/expected-room-conditions/1";

            var response = await _client.GetAsync(path);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task GetExpectedRoomConditions_WhenNoExpectedRoomConditionsFound_ShouldReturnNotFound()
        {
            var path = $"policies-api/expected-room-conditions/2";

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

            await AppContext.SaveChangesAsync();
        }
        
        internal async Task AddExpectedRoomConditionsToTheDatabase()
        {
            var expectedRoomConditions = new ExpectedRoomConditions
            {
                Id = 1,
                Temperature = 20,
                Illuminance = 100,
                Humidity = (decimal) 0.4,
                TemperatureMargin = 2,
                IlluminanceMargin = 20,
                HumidityMargin = (decimal) 0.1
            };

            await AppContext.ExpectedRoomConditions.AddAsync(expectedRoomConditions);
        }
    }
}