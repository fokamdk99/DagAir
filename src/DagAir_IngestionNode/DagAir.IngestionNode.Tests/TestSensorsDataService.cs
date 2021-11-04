using System;
using System.Threading.Tasks;
using DagAir.IngestionNode.Integrations.Sensors.DataServices;
using DagAir.Sensors.Contracts.DTOs;

namespace DagAir.IngestionNode.Tests
{
    public class TestSensorsDataService : ISensorsDataService
    {
        public async Task<SensorDto> GetSensorById(string id)
        {
            return CreateNewSensorDto();
        }

        internal static SensorDto CreateNewSensorDto()
        {
            return new SensorDto()
            {
                Id = 1,
                LastDataSentDate = DateTime.Now.AddMinutes(-5),
                RoomId = 1,
                AffiliateId = 1,
                SensorModelId = 1
            };
        }
    }
}