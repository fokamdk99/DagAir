using System;
using System.Threading.Tasks;
using DagAir.Policies.Contracts.DTOs;
using DagAir.PolicyNode.Integrations.Policies.DataServices;

namespace DagAir.PolicyNode.Tests
{
    internal class TestPoliciesDataService : IPoliciesDataService
    {
        public async Task<RoomPolicyDto> GetRoomPolicyByRoomId(long roomId)
        {
            return new RoomPolicyDto()
            {
                Id = 1,
                StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                    DateTime.Now.Minute, DateTime.Now.Second),
                EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                    DateTime.Now.AddHours(2).Hour, DateTime.Now.Minute, DateTime.Now.Second),
                RepeatOn = "Monday, Thursday",
                ExpectedConditionsId = 1L,
                CategoryId = 1L,
                RoomId = 1L,
                ExpectedConditions = new ExpectedRoomConditionsDto()
                {
                    Id = 1,
                    Temperature = 20,
                    Illuminance = 100,
                    Humidity = 0.45f,
                    TemperatureMargin = 2,
                    IlluminanceMargin = 20,
                    HumidityMargin = 0.1f
                },
                Category = new RoomPolicyCategoryDto()
                {
                    Id = 1,
                    Name = "Default",
                    CategoryNumber = 0
                }
            };
        }
    }
}