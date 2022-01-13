using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DagAir.Components.HttpClients;
using DagAir.Policies.Contracts.Commands;
using DagAir.Policies.Contracts.DTOs;
using DagAir.Policies.Data.AppEntities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace DagAir.Policies.Tests.Policies
{
    public class AddNewRoomPolicyTests : IntegrationTestServer
    {
        private DagAirHttpClient _dagAirHttpClient;
        
        [Test]
        public async Task WhenAddNewRoomPolicyCommandReceived_ShouldCreateNewRoomPolicyInTheDatabase()
        {
            var path = $"policies-api/policies";

            var addNewRoomPolicyCommand = CreateAddNewRoomPolicyCommand();
            (var response, var statusCode) = await _dagAirHttpClient.PostAsync<AddNewRoomPolicyCommand, RoomPolicyDto>(path, addNewRoomPolicyCommand);

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
            await AddRoomPolicyCategoriesToTheDatabase();

            await AppContext.SaveChangesAsync();
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

        public AddNewRoomPolicyCommand CreateAddNewRoomPolicyCommand()
        {
            return new AddNewRoomPolicyCommand
            {
                RoomPolicyDto = new RoomPolicyDto()
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(2),
                    RepeatOn = "",
                    RoomId = 1
                },
                ExpectedRoomConditionsDto = new ExpectedRoomConditionsDto
                {
                    Temperature = 24,
                    Illuminance = 160,
                    Humidity = (decimal)0.4,
                    TemperatureMargin = 3,
                    IlluminanceMargin = 10,
                    HumidityMargin = (decimal)0.1
                },
                RoomPolicyCategoryDto = new RoomPolicyCategoryDto
                {
                    Id = 1
                }
            };
        }
    }
}