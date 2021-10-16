﻿#nullable enable
using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;
using DagAir.MassTransit.RabbitMq.Publisher;
using DagAir.PolicyNode.Consumers;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DagAir.PolicyNode.Tests
{
    public class ConsumeSaveMeasurementsTests : MassTransitIntegrationTest
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
            services.AddScoped<IEventPublisher, EventPublisher>();
            services.AddMassTransitInMemoryTestHarness(cfg =>
            {
                cfg.AddConsumer<MeasurementSentEventConsumer>();
                cfg.AddConsumerTestHarness<MeasurementSentEventConsumer>();
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