using System.Net;
using System.Threading.Tasks;
using DagAir.Components.HttpClients;
using DagAir.Policies.Contracts.Commands;
using DagAir.Policies.Contracts.DTOs;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace DagAir.Policies.Tests.RoomConditions
{
    public class AddNewExpectedRoomConditionsTests : IntegrationTestServer
    {
        private DagAirHttpClient _dagAirHttpClient;
        
        [Test]
        public async Task WhenAddNewRoomPolicyCommandReceived_ShouldCreateNewRoomPolicyInTheDatabase()
        {
            var path = $"policies-api/expected-room-conditions";

            var addNewRoomPolicyCommand = CreateAddNewExpectedRoomConditionsCommand();
            (var response, var statusCode) = await _dagAirHttpClient.PostAsync<AddNewExpectedRoomConditionsCommand, ExpectedRoomConditionsDto>(path, addNewRoomPolicyCommand);

            statusCode.Should().Be(HttpStatusCode.Created);
            response.Id.Should().BeGreaterThan(0);
        }

        public override async Task OneTimeSetup()
        {
            await base.OneTimeSetup();

            var client = PoliciesApiTestClient.GetTestClient(TestServer, Configuration);
            _dagAirHttpClient = new DagAirHttpClient(client, new Logger<DagAirHttpClient>(new LoggerFactory()));
        }
        
        public override async Task Setup()
        {
            await AddDataToTheDatabase();
        }
        
        private async Task AddDataToTheDatabase()
        {
            await AppContext.Database.EnsureDeletedAsync();

            await AppContext.SaveChangesAsync();
        }

        AddNewExpectedRoomConditionsCommand CreateAddNewExpectedRoomConditionsCommand()
        {
            return new()
            {
                ExpectedRoomConditionsDto = new ExpectedRoomConditionsDto
                {
                    Temperature = 20,
                    Illuminance = 100,
                    Humidity = 0.4f,
                    TemperatureMargin = 2,
                    IlluminanceMargin = 20,
                    HumidityMargin = 0.1f
                }
            };
        }
    }
}