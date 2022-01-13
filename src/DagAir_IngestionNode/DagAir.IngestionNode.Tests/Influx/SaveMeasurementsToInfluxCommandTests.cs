using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;
using DagAir.IngestionNode.Influx.Handlers;
using DagAir.IngestionNode.Measurements.Commands;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DagAir.IngestionNode.Tests.Influx
{
    public class SaveMeasurementsToInfluxCommandTests : InfluxIntegrationTest
    {
        [Ignore("Influx dependent")]
        [Test]
        public async Task WhenSaveMeasurementsToInfluxCommandHandled_ShouldCreateNewRecord()
        {
            var query = $"from(bucket: \"{TestBucket.Name}\") |> range(start: 0)";
            var temperatureQuery = CreateInfluxQuery("temperature");
            var illuminanceQuery = CreateInfluxQuery("illuminance");
            var humidityQuery = CreateInfluxQuery("humidity");
            var organizationId = await InfluxHelper.GetOrganizationIdByOrganizationName(Client, InfluxConfiguration);
            var queryResultsBeforeWrite = await Client.GetQueryApi().QueryAsync(query, organizationId);
            int numberOfRowsBeforeWrite = queryResultsBeforeWrite.Count;
            
            var command = new SaveMeasurementsToInfluxHandler(Client, InfluxConfiguration);
            var insertedEvent = CreateMockMeasurementsInsertedEvent();
            await command.Handle(insertedEvent);
            Thread.Sleep(3000); //TODO: investigate the issue
            var queryResultsAfterWrite = await Client.GetQueryApi().QueryAsync(query, organizationId);
            int numberOfRowsAfterWrite = queryResultsAfterWrite.Count;

            Assert.AreEqual(numberOfRowsBeforeWrite + 3, numberOfRowsAfterWrite);
            
            var temperatureResults = await Client.GetQueryApi().QueryAsync(temperatureQuery, organizationId);

            Assert.AreEqual(temperatureResults.ElementAt(0).Records.ElementAt(0).Values["_value"],
                insertedEvent.Measurement.Temperature);

            var illuminanceResults = await Client.GetQueryApi().QueryAsync(illuminanceQuery, organizationId);
            
            Assert.AreEqual(illuminanceResults.ElementAt(0).Records.ElementAt(0).Values["_value"],
                insertedEvent.Measurement.Illuminance);

            var humidityResults = await Client.GetQueryApi().QueryAsync(humidityQuery, organizationId);
            
            Assert.AreEqual(humidityResults.ElementAt(0).Records.ElementAt(0).Values["_value"],
                insertedEvent.Measurement.Humidity);
        }

        private string CreateInfluxQuery(string field)
        {
            return $"from(bucket: \"{TestBucket.Name}\")\n |> range(start: 0)"
                   + $" |> filter(fn: (r) => (r[\"_measurement\"] == \"influxroommeasurement\" and r[\"_field\"] == \"{field}\"))";
        }
        
        private NewMeasurementReceivedCommand CreateMockMeasurementsInsertedEvent()
        {
            var measurement = new RoomMeasurement((decimal) 18, (int) 0.8, (decimal) 55.5);
            return new NewMeasurementReceivedCommand(measurement, "id_1");
        }

        protected override async Task SetupPreHost()
        {
            
        }
        
        protected override void AddOverrides(IServiceCollection services)
        {
            
        }
    }
}