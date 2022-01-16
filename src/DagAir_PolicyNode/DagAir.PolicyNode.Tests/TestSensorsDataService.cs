using System;
using System.Threading.Tasks;
using DagAir.PolicyNode.Integrations.Sensors.DataServices;
using DagAir.Sensors.Contracts.DTOs;

namespace DagAir.PolicyNode.Tests
{
    public class TestSensorsDataService : ISensorsDataService
    {
        public async Task<SensorDto> GetSensorBySensorName(string sensorName)
        {
            return CreateTestSensorDto();
        }

        private SensorDto CreateTestSensorDto()
        {
            var sensor = new SensorDto()
            {
                Id = 2,
                SensorName = "wemos_stas1",
                LastDataSentDate = DateTime.Now.AddHours(-3),
                RoomId = 1,
                AffiliateId = 1,
                SensorModelId = 2
            };

            return sensor;
        }
    }
}