using System.Threading.Tasks;
using DagAir.Components.MassTransit.RabbitMq.Publisher;
using DagAir.IngestionNode.Contracts;
using DagAir.PolicyNode.Consumers;
using DagAir.PolicyNode.Integrations.Policies.DataServices;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DagAir.PolicyNode.Tests.Consumers
{
    public class MeasurementSentEventConsumerTests : MassTransitIntegrationTest
    {
        private InMemoryTestHarness? _testHarness;
        
        [Test]
        public async Task WhenNewMeasurementSentFromSensor_ShouldPublishMeasurementsInsertedEvent()
        {
            var @event = CreateMeasurementSentEvent();
            
            await PublisherHelper.PublishAndWaitToBeConsumed(@event, _testHarness);
            
            Assert.Pass();
        }

        private MeasurementSentEvent CreateMeasurementSentEvent()
        {
            return new MeasurementSentEvent(21, 2000, 0.5F, 1);
        }

        protected override void AddOverrides(IServiceCollection services)
        {
            services.AddScoped<IEventPublisher, EventPublisher>();
            services.AddScoped<IPoliciesDataService, TestPoliciesDataService>();

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