using System.Threading.Tasks;
using DagAir.Components.Influx;
using DagAir.DataServices.SensorStateHistory.Data.Measurements;
using DagAir.DataServices.SensorStateHistory.Influx.Commands;
using DagAir.IngestionNode.Contracts;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;

namespace DagAir.DataServices.SensorStateHistory.Influx.Handlers
{
    public class SaveMeasurementsToInfluxHandler : ISaveMeasurementsToInfluxHandler
    {
        private readonly InfluxDBClient _client;
        private readonly IInfluxConfiguration _influxConfiguration;

        public SaveMeasurementsToInfluxHandler(InfluxDBClient client, 
            IInfluxConfiguration influxConfiguration)
        {
            _client = client;
            _influxConfiguration = influxConfiguration;
        }
        
        public async Task Handle(SaveMeasurementToInfluxDBEvent measurementsInsertedEvent)
        {
            var measurement = new InfluxRoomMeasurement()
                {
                    SensorName = measurementsInsertedEvent.SensorName,
                    Temperature = measurementsInsertedEvent.Temperature, 
                    Humidity = measurementsInsertedEvent.Humidity, 
                    Illuminance = measurementsInsertedEvent.Illuminance,
                };

            await _client.GetWriteApiAsync().WriteMeasurementAsync(_influxConfiguration.BucketName, _influxConfiguration.Org, WritePrecision.Ms, measurement);
        }
    }
    
}