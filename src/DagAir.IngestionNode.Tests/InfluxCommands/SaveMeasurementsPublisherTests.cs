#nullable enable
using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DagAir.IngestionNode.Tests.InfluxCommands
{
    public class SaveMeasurementsPublisherTests : MassTransitIntegrationTest
    {
        private InMemoryTestHarness? _testHarness;

        [Test]
        public async Task WhenNewMeasurementSentFromSensor_ShouldPublishMeasurementsInsertedEvent()
        {
            var @event = CreateMeasurementsInsertedEvent();
            
            await PublisherHelper.PublishAndWaitToBeConsumed(@event, _testHarness);
        }

        private MeasurementsInsertedEvent CreateMeasurementsInsertedEvent()
        {
            var measurement = new RoomMeasurement(12, 13, 14);
            string sensorId = "sensor_nr1";
            return new MeasurementsInsertedEvent(measurement, sensorId);
        }

        protected override void AddOverrides(IServiceCollection services)
        {
            services.AddMassTransitInMemoryTestHarness();
        }

        protected override async Task SetupTest()
        {
            _testHarness = Services.GetRequiredService<InMemoryTestHarness>();

            await _testHarness.Start();
        }

        [TearDown]
        protected override void CleanUp()
        {
            _testHarness!.Stop();
            base.CleanUp();
        }
    }
}