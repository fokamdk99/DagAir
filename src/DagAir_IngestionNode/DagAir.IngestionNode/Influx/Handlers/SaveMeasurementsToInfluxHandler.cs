using System;
using System.Threading.Tasks;
using DagAir.IngestionNode.Data.Influx;
using DagAir.IngestionNode.Data.Measurements;
using DagAir.Components.MassTransit.RabbitMq.Publisher;
using DagAir.IngestionNode.Measurements.Commands;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;

namespace DagAir.IngestionNode.Influx.Handlers
{
    public class SaveMeasurementsToInfluxHandler : ISaveMeasurementsToInfluxHandler
    {
        private readonly InfluxDBClient _client;
        private readonly IInfluxConfiguration _influxConfiguration;
        private readonly IEventPublisher _eventPublisher;

        public SaveMeasurementsToInfluxHandler(InfluxDBClient client, IInfluxConfiguration influxConfiguration, IEventPublisher eventPublisher)
        {
            _client = client;
            _influxConfiguration = influxConfiguration;
            _eventPublisher = eventPublisher;
        }
        
        public async Task Handle(NewMeasurementReceivedCommand measurementsInsertedEvent)
        {
            var measurement = new InfluxRoomMeasurement()
                {
                    SensorId = measurementsInsertedEvent.SensorId,
                    Temperature = measurementsInsertedEvent.Measurement.Temperature, 
                    Humidity = measurementsInsertedEvent.Measurement.Humidity, 
                    Illuminance = measurementsInsertedEvent.Measurement.Illuminance, 
                    Time = DateTime.UtcNow
                };
                
                await _client.GetWriteApiAsync().WriteMeasurementAsync(_influxConfiguration.BucketName, _influxConfiguration.Org, WritePrecision.Ms, measurement);
        }
    }
    
}