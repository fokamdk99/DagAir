using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.Policies.Data.AppEntities;
using DagAir.Policies.Policies.Queries;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DagAir.Policies.Tests.Policies.Queries
{
    public class GetPolicyHandlerTests : IntegrationTestServer
    {
        private IGetRoomPolicyQuery _getRoomPolicyQuery;
        
        [Test]
        public async Task WhenNoCurrentRoomPolicyAvailable_ShouldReturnDefaultRoomPolicy()
        {
            await AddDataToTheDatabase(AddOldRoomPoliciesToTheDatabase);

            var result = await _getRoomPolicyQuery.Handle(2, DateTime.Now);
            
            Assert.AreEqual(4, result.Id);
        }
        
        [Test]
        public async Task WhenCurrentRoomPolicyAvailable_ShouldReturnCurrentRoomPolicy()
        {
            await AddDataToTheDatabase(AddEveryDayPolicyToTheDatabase);

            var time = new DateTime(2021, 12, 5, 11, 1, 1);
            var result = await _getRoomPolicyQuery.Handle(1, time);
            
            Assert.AreEqual(3, result.Id);
        }
        
        [Test]
        public async Task WhenRoomPolicyThatSpansTwoDaysAvailable_ShouldReturnCurrentRoomPolicy()
        {
            await AddDataToTheDatabase(AddRoomPolicyThatSpansTwoDaysToTheDatabase);

            var time = new DateTime(2022, 1, 14, 3, 1, 1);
            var result = await _getRoomPolicyQuery.Handle(1, time);
            
            Assert.AreEqual(1, result.Id);
        }

        protected override async Task Setup()
        {
            _getRoomPolicyQuery = _scope.ServiceProvider.GetRequiredService<IGetRoomPolicyQuery>();
        }
        
        private async Task AddDataToTheDatabase(Func<Task> addRoomPolicies)
        {
            await AppContext.Database.EnsureDeletedAsync();
            await addRoomPolicies();
            await AddRoomPolicyCategories();
            await AddExpectedRoomConditions();

            await AppContext.SaveChangesAsync();
        }
        
        private async Task AddOldRoomPoliciesToTheDatabase()
        {
            var roomPolicies = new List<RoomPolicy>();
            roomPolicies.Add(new RoomPolicy()
            {
                Id = 3,
                StartDate = new DateTime(1990, 11, 5, 1, 1, 1),
                EndDate = new DateTime(1991, 10, 5, 23, 59, 59),
                StartHour = 22,
                EndHour = 6,
                RepeatOn = "",
                ExpectedConditionsId = 2L,
                CategoryId = 2L,
                RoomId = 1L,
                CreatedBy = "dd8db40b-c091-4d12-a185-b2f71f369917",
                SpansTwoDays = true
            });
            roomPolicies.Add(new RoomPolicy()
            {
                Id = 4,
                StartDate = new DateTime(1980, 11, 5, 1, 1, 1),
                EndDate = new DateTime(9999, 12, 30, 23, 59, 59),
                StartHour = 0,
                EndHour = 23,
                RepeatOn = "",
                ExpectedConditionsId = 2L,
                CategoryId = 1L,
                RoomId = 0,
                CreatedBy = "dd8db40b-c091-4d12-a185-b2f71f369917",
                SpansTwoDays = false
            });

            await AppContext.RoomPolicies.AddRangeAsync(roomPolicies);
        }
        
        private async Task AddEveryDayPolicyToTheDatabase()
        {
            var roomPolicies = new List<RoomPolicy>();
            roomPolicies.Add(new RoomPolicy()
            {
                Id = 3,
                StartDate = new DateTime(2021, 11, 5, 1, 1, 1),
                EndDate = new DateTime(2025, 10, 5, 23, 59, 59),
                StartHour = 10,
                EndHour = 14,
                RepeatOn = "",
                ExpectedConditionsId = 2L,
                CategoryId = 2L,
                RoomId = 1L,
                CreatedBy = "dd8db40b-c091-4d12-a185-b2f71f369917",
                SpansTwoDays = false
            });
            roomPolicies.Add(new RoomPolicy()
            {
                Id = 4,
                StartDate = new DateTime(1980, 11, 5, 1, 1, 1),
                EndDate = new DateTime(9999, 12, 30, 23, 59, 59),
                StartHour = 0,
                EndHour = 23,
                RepeatOn = "",
                ExpectedConditionsId = 2L,
                CategoryId = 1L,
                RoomId = 0,
                CreatedBy = "dd8db40b-c091-4d12-a185-b2f71f369917",
                SpansTwoDays = false
            });

            await AppContext.RoomPolicies.AddRangeAsync(roomPolicies);
        }
        
        private async Task AddRoomPolicyThatSpansTwoDaysToTheDatabase()
        {
            var roomPolicies = new List<RoomPolicy>();
            roomPolicies.Add(new RoomPolicy()
            {
                Id = 1,
                StartDate = new DateTime(2021, 11, 5, 1, 1, 1),
                EndDate = new DateTime(2025, 10, 5, 23, 59, 59),
                StartHour = 22,
                EndHour = 6,
                RepeatOn = "Thursday",
                ExpectedConditionsId = 2L,
                CategoryId = 2L,
                RoomId = 1L,
                CreatedBy = "dd8db40b-c091-4d12-a185-b2f71f369917",
                SpansTwoDays = true
            });
            roomPolicies.Add(new RoomPolicy()
            {
                Id = 2,
                StartDate = new DateTime(1980, 11, 5, 1, 1, 1),
                EndDate = new DateTime(9999, 12, 30, 23, 59, 59),
                StartHour = 0,
                EndHour = 23,
                RepeatOn = "",
                ExpectedConditionsId = 2L,
                CategoryId = 1L,
                RoomId = 0,
                CreatedBy = "dd8db40b-c091-4d12-a185-b2f71f369917",
                SpansTwoDays = false
            });

            await AppContext.RoomPolicies.AddRangeAsync(roomPolicies);
        }

        private async Task AddRoomPolicyCategories()
        {
            var roomPolicyCategories = new List<RoomPolicyCategory>
            {
                new RoomPolicyCategory()
                {
                    Id = 1,
                    Name = "Default",
                    CategoryNumber = 0
                },
                new RoomPolicyCategory()
                {
                    Id = 2,
                    Name = "Organizational",
                    CategoryNumber = 1
                },
                new RoomPolicyCategory()
                {
                    Id = 3,
                    Name = "Departmental",
                    CategoryNumber = 2
                },
                new RoomPolicyCategory()
                {
                    Id = 4,
                    Name = "Custom",
                    CategoryNumber = 3
                }
            };

            await AppContext.RoomPolicyCategories.AddRangeAsync(roomPolicyCategories);
        }

        private async Task AddExpectedRoomConditions()
        {
            var expectedRoomConditions = new List<ExpectedRoomConditions>
            {
                new ExpectedRoomConditions()
                {
                    Id = 1,
                    Temperature = 20,
                    Illuminance = 100,
                    Humidity = (decimal) 0.4,
                    TemperatureMargin = 2,
                    IlluminanceMargin = 20,
                    HumidityMargin = (decimal) 0.1
                },
                new ExpectedRoomConditions()
                {
                    Id = 2,
                    Temperature = 22,
                    Illuminance = 130,
                    Humidity = (decimal) 0.5,
                    TemperatureMargin = 3,
                    IlluminanceMargin = 30,
                    HumidityMargin = (decimal) 0.1
                }
            };
            
            await AppContext.ExpectedRoomConditions.AddRangeAsync(expectedRoomConditions);
        }
    }
}