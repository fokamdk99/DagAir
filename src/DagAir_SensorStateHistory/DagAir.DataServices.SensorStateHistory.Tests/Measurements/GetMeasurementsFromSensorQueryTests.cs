using System.Threading.Tasks;
using DagAir.DataServices.SensorStateHistory.Contracts.Commands;
using DagAir.DataServices.SensorStateHistory.Measurements.Queries;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DagAir.DataServices.SensorStateHistory.Tests.Measurements
{
    public class GetMeasurementsFromSensorQueryTests : IntegrationTest
    {
        private IGetMeasurementsFromSensorQuery _getMeasurementsFromSensorQuery;
        
        [Ignore("Influx dependent")]
        [Test]
        public async Task WhenValidGetMeasurementsFromSensorCommandGiven_ShouldReturnRecordsFromInfluxDB()
        {
            var getMeasurementsFromSensorCommand = GetTestMeasurementsFromSensorQueryCommand("wemos_stas1", 3);

            var historicResults = await _getMeasurementsFromSensorQuery.GetMeasurementsFromSensor(getMeasurementsFromSensorCommand);
            
            Assert.GreaterOrEqual(historicResults.Count, 1);
        }
        
        [Ignore("Influx dependent")]
        [Test]
        public async Task WhenNoRecordsFulfillCriteria_ShouldReturnRecordsFromInfluxDB_ShouldReturnEmptyList()
        {
            var getMeasurementsFromSensorCommand = GetTestMeasurementsFromSensorQueryCommand("ababababab", 3);

            var historicResults = await _getMeasurementsFromSensorQuery.GetMeasurementsFromSensor(getMeasurementsFromSensorCommand);
            
            Assert.AreEqual(0, historicResults.Count);
        }
        
        protected override void AddOverrides(IServiceCollection services)
        {
            
        }

        protected override Task SetupTest()
        {
            _getMeasurementsFromSensorQuery = Services.GetRequiredService<IGetMeasurementsFromSensorQuery>();
            return base.SetupTest();
        }

        private GetMeasurementsFromSensorCommand GetTestMeasurementsFromSensorQueryCommand(
            string sensorName,
            int numberOfRecords)
        {
            return new GetMeasurementsFromSensorCommand
            {
                SensorName = sensorName,
                NumberOfRecords = numberOfRecords
            };
        }
    }
}