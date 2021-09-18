using System;
using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;
using DagAir.IngestionNode.Data.Measurements;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;

namespace DagAir.IngestionNode.Data.Queries
{
    public class SaveMeasurementsToInfluxCommand : ISaveMeasurementsToInflux
    {
        private readonly char[] _token;
        
        private readonly InfluxDBClient _client;

        public SaveMeasurementsToInfluxCommand(char[] token, InfluxDBClient client)
        {
            _token = token;
            _client = client;
        }
        
        public async Task Handle(IMeasurementsInsertedEvent measurementsInsertedEvent)
        {
            using (var writeApi = _client.GetWriteApi())
            {
                var mem = new InfluxRoomMeasurement()
                {
                    SensorId = measurementsInsertedEvent.SensorIdentity.Id, 
                    SensorName = measurementsInsertedEvent.SensorIdentity.Name,
                    Temperature = measurementsInsertedEvent.Measurement.Temperature, 
                    Humidity = measurementsInsertedEvent.Measurement.Humidity, 
                    Illuminance = measurementsInsertedEvent.Measurement.Illuminance, 
                    Time = DateTime.UtcNow
                };
                
                writeApi.WriteMeasurement("dagair_bucket", "dagair", WritePrecision.Ms, mem);
            }
        }
    }
    
}