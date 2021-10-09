using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;
using DagAir.IngestionNode.Data.Measurements;
using DagAir.IngestionNode.InfluxCommands;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DagAir.IngestionNode.Tests.InfluxCommands
{
    public class WriteDataToInfluxTests : IntegrationTest
    {
        [Test]
        public async Task WhenSaveMeasurementsInfluxToInfluxCommandHandled_ShouldCreateNewRecord()
        {
            var query = $"from(bucket: \"{TestBucket.Name}\") |> range(start: 0)";
            var tables = await Client.GetQueryApi().QueryAsync(query, InfluxConfiguration.OrgId);
            int numberOfRowsBeforeWrite = tables.Count;

            var command = new SaveMeasurementsToInfluxCommand(Client, InfluxConfiguration);
            var insertedEvent = CreateMockMeasurementsInsertedEvent();
            
            await command.Handle(insertedEvent);

            tables = await Client.GetQueryApi().QueryAsync(query, InfluxConfiguration.OrgId);
            int numberOfRowsAfterWrite = tables.ElementAt(0).Records.Count;

            Assert.AreEqual(numberOfRowsBeforeWrite + 1, numberOfRowsAfterWrite);
        }

        private MeasurementsInsertedEvent CreateMockMeasurementsInsertedEvent()
        {
            var measurement = new RoomMeasurement((float) 17.5, (float) 0.8, (float) 55.5);
            return new MeasurementsInsertedEvent(measurement, "id_1");
        }
        
        protected override void AddOverrides(IServiceCollection services)
        {
            
        }
    }
}