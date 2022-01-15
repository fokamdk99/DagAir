using System;
using System.Threading.Tasks;
using DagAir.Components.Influx;
using DagAir.IngestionNode.Data.Measurements;
using DagAir.IngestionNode.Measurements.Commands;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;

namespace DagAir.IngestionNode.Influx.Handlers
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
        
        public async Task Handle(NewMeasurementReceivedCommand measurementsInsertedEvent)
        {
            var measurement = new InfluxRoomMeasurement()
                {
                    SensorName = measurementsInsertedEvent.SensorName,
                    Temperature = measurementsInsertedEvent.Measurement.Temperature, 
                    Humidity = measurementsInsertedEvent.Measurement.Humidity, 
                    Illuminance = measurementsInsertedEvent.Measurement.Illuminance,
                };

            await _client.GetWriteApiAsync().WriteMeasurementAsync(_influxConfiguration.BucketName, _influxConfiguration.Org, WritePrecision.Ms, measurement);
        }
    }
    
}