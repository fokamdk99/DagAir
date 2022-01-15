using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.DataServices.SensorStateHistory.Contracts.Contracts;
using DagAir.Policies.Contracts.Commands;
using DagAir.Policies.Data.AppEntities;
using DagAir.Policies.Policies;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DagAir.Policies.Tests.Policies.Commands
{
    public class GetPastPoliciesCommandHandlerTests : IntegrationTestServer
    {
        private ICommandHandler<GetPastPoliciesCommand, PastMeasurements> _getPastPoliciesCommandHandler;

        [Test]
        public async Task WhenGetPastPoliciesCommandReceived_ShouldReturnValidPastMeasurements()
        {
            await AddDataToTheDatabase(AddEveryDayPolicyToTheDatabase);

            var getPastPoliciesCommand = CreateTestGetPastPoliciesCommand();
            var result = await _getPastPoliciesCommandHandler.Handle(getPastPoliciesCommand);

            Assert.AreEqual(2, result.Measurements.Count);
        }
        
        protected override async Task Setup()
        {
            _getPastPoliciesCommandHandler = Services.GetRequiredService<ICommandHandler<GetPastPoliciesCommand, PastMeasurements>>();
        }
        
        private async Task AddDataToTheDatabase(Func<Task> addRoomPolicies)
        {
            await AppContext.Database.EnsureDeletedAsync();
            await addRoomPolicies();
            await AddRoomPolicyCategories();
            await AddExpectedRoomConditions();

            await AppContext.SaveChangesAsync();
        }

        private GetPastPoliciesCommand CreateTestGetPastPoliciesCommand()
        {
            var getPastPoliciesCommand = new GetPastPoliciesCommand
            {
                RoomId = 1,
                HistoricMeasurements = new List<HistoricMeasurement>
                {
                    new HistoricMeasurement((decimal) 23.95,
                        438,
                        (decimal) 45.48,
                        new DateTime(2022, 11, 5, 11, 59, 59)),
                    new HistoricMeasurement((decimal) 33.95,
                        538,
                        (decimal) 55.48,
                        new DateTime(1999, 11, 5, 23, 59, 59)),
                }
            };

            return getPastPoliciesCommand;
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