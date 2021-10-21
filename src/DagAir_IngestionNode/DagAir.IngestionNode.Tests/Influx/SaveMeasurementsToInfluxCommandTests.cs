using System.Linq;
using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;
using DagAir.IngestionNode.Influx.Handlers;
using DagAir.Components.MassTransit.RabbitMq.Publisher;
using DagAir.IngestionNode.Measurements.Commands;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DagAir.IngestionNode.Tests.Influx
{
    public class WriteDataToInfluxTests : IntegrationTest
    {
        [Test]
        public async Task WhenSaveMeasurementsInfluxToInfluxCommandHandled_ShouldCreateNewRecord()
        {
            var query = $"from(bucket: \"{TestBucket.Name}\") |> range(start: 0)";
            var tables = await Client.GetQueryApi().QueryAsync(query, InfluxConfiguration.OrgId);
            int numberOfRowsBeforeWrite = tables.Count;

            var command = new SaveMeasurementsToInfluxHandler(Client, InfluxConfiguration, Services.GetRequiredService<IEventPublisher>());
            var insertedEvent = CreateMockMeasurementsInsertedEvent();
            
            await command.Handle(insertedEvent);

            tables = await Client.GetQueryApi().QueryAsync(query, InfluxConfiguration.OrgId);
            int numberOfRowsAfterWrite = tables.ElementAt(0).Records.Count;

            Assert.AreEqual(numberOfRowsBeforeWrite + 1, numberOfRowsAfterWrite);
        }

        private NewMeasurementReceivedCommand CreateMockMeasurementsInsertedEvent()
        {
            var measurement = new RoomMeasurement((float) 17.5, (float) 0.8, (float) 55.5);
            return new NewMeasurementReceivedCommand(measurement, "id_1");
        }
        
        protected override void AddOverrides(IServiceCollection services)
        {
            
        }
    }
}