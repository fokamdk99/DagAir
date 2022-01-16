using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DagAir.Sensors.Contracts.Commands;
using DagAir.Sensors.Data.AppEntities;
using FluentAssertions;
using NUnit.Framework;

namespace DagAir.Sensors.Tests.Sensors
{
    public class SensorsEndpointPathsTests : IntegrationTestServer
    {
        private HttpClient _client;

        [Test]
        public async Task GetAllSensorsWithRelatedEntitesQuery_ShouldBeAvailableUnderDefinedPath()
        {
            var path = $"sensors-api/sensors";

            var response = await _client.GetAsync(path);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Test]
        public async Task GetSensorWithRelatedEntitesQuery_ShouldBeAvailableUnderDefinedPath()
        {
            var path = $"sensors-api/sensors/1";

            var response = await _client.GetAsync(path);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task GetSensorWithRelatedEntitesQuery_WhenInvalidIdGiven_ShouldReturnNotFound()
        {
            var path = $"sensors-api/sensors/98765432123456789";

            var response = await _client.GetAsync(path);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        
        [Test]
        public async Task GetSensorByUniqueRoomIdQuery_ShouldBeAvailableUnderDefinedPath()
        {
            var path = $"sensors-api/sensors/sensor-name";

            var getSensorBySensorNameCommand = new GetSensorBySensorNameCommand
            {
                SensorName = "wemos_stas1"
            };

            var request = 
                new StringContent(JsonSerializer.Serialize(getSensorBySensorNameCommand), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(path, request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        public override async Task Setup()
        {
            await AddDataToTheDatabase();
        }

        public override async Task OneTimeSetup()
        {
            await base.OneTimeSetup();

            _client = SensorsApiTestClient.GetTestClient(TestServer, Configuration);
        }

        private async Task AddDataToTheDatabase()
        {
            await AppContext.Database.EnsureDeletedAsync();
            await AddSensorToTheDatabase();
            await AppContext.SaveChangesAsync();
        }

        private async Task AddSensorToTheDatabase()
        {
            var sensor = new Sensor()
            {
                Id = 1,
                SensorName = "wemos_stas1",
                LastDataSentDate = DateTime.Now.AddHours(-5),
                RoomId = 1,
                AffiliateId = 1,
                SensorModelId = 1
            };

            await AppContext.Sensors.AddAsync(sensor);
        }
    }
}