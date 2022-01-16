﻿using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DagAir.Components.ApiModels.Json;
using DagAir.Policies.Contracts.DTOs;
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
        public async Task WhenNoCurrentRoomPolicyFoundForRoom_ShouldReturnDefaultRoomPolicy()
        {
            var path = $"policies-api/policies/9898989898989";

            var response = await _client.GetAsync(path);

            var contentStream = await response.Content.ReadAsStreamAsync();
            var content = await JsonSerializer.DeserializeAsync<JsonApiDocument<RoomPolicyDto>>(contentStream,new JsonSerializerOptions { IgnoreNullValues = true, PropertyNameCaseInsensitive = true });
            
            content!.Data.Category.Name.Should().Be("Default");
        }

        protected override async Task Setup()
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

        internal async Task AddPoliciesToTheDatabase()
        {
            var policy = new RoomPolicy()
            {
                Id = 1,
                StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.AddHours(-3).Hour,
                    DateTime.Now.Minute, DateTime.Now.Second),
                EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                    DateTime.Now.AddHours(3).Hour, DateTime.Now.Minute, DateTime.Now.Second),
                StartHour = 2,
                EndHour = 20,
                RepeatOn = "",
                ExpectedConditionsId = 1L,
                CategoryId = 1,
                RoomId = 1
            };

            await AppContext.RoomPolicies.AddAsync(policy);
        }
        
        internal async Task AddRoomPolicyCategoriesToTheDatabase()
        {
            var roomPolicyCategory = new RoomPolicyCategory()
            {
                Id = 1,
                Name = "Default",
                CategoryNumber = 0
            };

            await AppContext.RoomPolicyCategories.AddAsync(roomPolicyCategory);
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