#nullable enable
using System.Threading.Tasks;
using DagAir.Components.MassTransit.RabbitMq.Publisher;
using DagAir.DataServices.SensorStateHistory.Consumers;
using DagAir.DataServices.SensorStateHistory.Influx.Handlers;
using DagAir.IngestionNode.Contracts;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DagAir.DataServices.SensorStateHistory.Tests.Consumers
{
    public class MeasurementSentEventConsumerTests : IntegrationTest
    {
        private InMemoryTestHarness? _testHarness;
        
        [Test]
        public async Task WhenNewMeasurementSentFromSensor_ShouldPublishMeasurementsInsertedEvent()
        {
            var @event = CreateMeasurementSentEvent();
            
            await PublisherHelper.PublishAndWaitToBeConsumed(@event, _testHarness);

            await PublisherHelper.CheckThatEventIsPublished<SaveMeasurementToInfluxDBEvent>(_testHarness);
        }

        private SaveMeasurementToInfluxDBEvent CreateMeasurementSentEvent()
        {
            return new SaveMeasurementToInfluxDBEvent(21, 500, (decimal) 0.5, "sensorName1");
        }

        protected override void AddOverrides(IServiceCollection services)
        {
            services.AddScoped<IEventPublisher, EventPublisher>();
            services.AddScoped<ISaveMeasurementsToInfluxHandler, TestSaveMeasurementsToInfluxHandler>();
            

            services.AddMassTransitInMemoryTestHarness(cfg =>
            {
                cfg.AddConsumer<SaveMeasurementToInfluxDBConsumer>();
                cfg.AddConsumerTestHarness<SaveMeasurementToInfluxDBConsumer>();
            });
        }

        protected override async Task SetupTest()
        {
            _testHarness = Services.GetRequiredService<InMemoryTestHarness>();

            await _testHarness!.Start();
        }

        [TearDown]
        protected override void CleanUp()
        {
            _testHarness!.Stop();
            base.CleanUp();
        }
    }
}