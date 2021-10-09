using System;
using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;
using DagAir.IngestionNode.Data.Influx;
using DagAir.IngestionNode.Data.Measurements;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;

namespace DagAir.IngestionNode.InfluxCommands
{
    public class SaveMeasurementsToInfluxCommand : ISaveMeasurementsToInfluxCommand
    {
        private readonly InfluxDBClient _client;
        private readonly IInfluxConfiguration _influxConfiguration;

        public SaveMeasurementsToInfluxCommand(InfluxDBClient client, IInfluxConfiguration influxConfiguration)
        {
            _client = client;
            _influxConfiguration = influxConfiguration;
        }
        
        public async Task Handle(IMeasurementsInsertedEvent measurementsInsertedEvent)
        {
            var mem = new InfluxRoomMeasurement()
                {
                    SensorId = measurementsInsertedEvent.SensorId,
                    Temperature = measurementsInsertedEvent.Measurement.Temperature, 
                    Humidity = measurementsInsertedEvent.Measurement.Humidity, 
                    Illuminance = measurementsInsertedEvent.Measurement.Illuminance, 
                    Time = DateTime.UtcNow
                };
                
                await _client.GetWriteApiAsync().WriteMeasurementAsync(_influxConfiguration.BucketName, _influxConfiguration.Org, WritePrecision.Ms, mem);
        }
    }
    
}