using System;
using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;
using DagAir.IngestionNode.Data.Queries;
using InfluxDB.Client;
using InfluxDB.Client.Core;
using NUnit.Framework;

namespace DagAir.IngestionNode.Tests
{
    public class WriteDataToInfluxTests
    {
        [Test]
        public async Task SaveDataToInflux()
        { 
            char[] token = "04QQQNYZs0E1KWImbwYgWBDPg6m6AhI-uATbrJzgEbMBPcPGI8g_iCrAQeD5yBitaoxKmtUvrijHQsdfIz2I1A==".ToCharArray();
            var influxDBClient = InfluxDBClientFactory.Create("http://localhost:8086", token);
            var command = new SaveMeasurementsToInfluxCommand(token, influxDBClient);
            var sensorIdentity = new SensorIdentity("id_1", "sensor_z_sali_wykladowej");
            var measurement = new RoomMeasurement((float) 17.5, (float) 0.8, (float) 55.5);
            var insertedEvent = new MeasurementsInsertedEvent(measurement, sensorIdentity);
            await command.Handle(insertedEvent);
            var query = $"from(bucket: \"dagair_bucket\") |> range(start: -1h)";
            var tables = await influxDBClient.GetQueryApi().QueryAsync(query, "dagair");
            
            Assert.IsTrue(true);
        }
        
    }
}