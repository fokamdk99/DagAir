using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DagAir.Policies.Contracts.Commands;
using DagAir.Policies.Contracts.DTOs;
using DagAir.Policies.Data.AppEntities;
using FluentAssertions;
using NUnit.Framework;

namespace DagAir.Policies.Tests.Policies
{
    public class AddNewRoomPolicyTests : IntegrationTestServer
    {
        private HttpClient _client;
        
        [Test]
        public async Task WhenAddNewRoomPolicyCommandReceived_ShouldCreateNewRoomPolicyInTheDatabase()
        {
            var path = $"policies-api/policies";

            var addNewRoomPolicyCommand = CreateAddNewRoomPolicyCommand();
            var postBody = new StringContent(JsonSerializer.Serialize(addNewRoomPolicyCommand), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(path, postBody);

            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        public override async Task OneTimeSetup()
        {
            await base.OneTimeSetup();

            _client = PoliciesApiTestClient.GetTestClient(TestServer, Configuration);
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
                    Humidity = 0.4f,
                    TemperatureMargin = 3,
                    IlluminanceMargin = 10,
                    HumidityMargin = 0.1f
                },
                RoomPolicyCategoryDto = new RoomPolicyCategoryDto
                {
                    Id = 1
                }
            };
        }
    }
}