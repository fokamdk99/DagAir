#nullable enable
using System.Threading.Tasks;
using DagAir.Components.MassTransit.RabbitMq.Publisher;
using DagAir.IngestionNode.Contracts;
using DagAir.IngestionNode.Influx.Handlers;
using DagAir.IngestionNode.Integrations.Sensors.DataServices;
using DagAir.IngestionNode.Measurements.Commands;
using DagAir.IngestionNode.Measurements.Handlers;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DagAir.IngestionNode.Tests.Handlers
{
    public class NewMeasurementReceivedHandlerTests : MassTransitIntegrationTest
    {
        private InMemoryTestHarness? _testHarness;
        private INewMeasurementReceivedHandler? _newMeasurementReceivedHandler;

        [Test]
        public async Task WhenNewMeasurementSentFromSensor_ShouldPublishMeasurementSentEvent()
        {
            var command = CreateMeasurementReceivedCommand();

            await _newMeasurementReceivedHandler!.Handle(command);

            await PublisherHelper.CheckThatEventIsPublished<MeasurementSentEvent>(_testHarness);
        }

        private NewMeasurementReceivedCommand CreateMeasurementReceivedCommand()
        {
            var testMeasurement = new TestMeasurement
            {
                Temperature = 21, 
                Illuminance = 120, 
                Humidity = (decimal) 0.4
            };
            return new NewMeasurementReceivedCommand(testMeasurement, "sensor_nr1");
        }

        protected override void AddOverrides(IServiceCollection services)
        {
            services.AddScoped<IEventPublisher, EventPublisher>();
            services.AddScoped<ISaveMeasurementsToInfluxHandler, TestSaveMeasurementsToInfluxHandler>();
            services.AddScoped<ISensorsDataService, TestSensorsDataService>();

            services.AddMassTransitInMemoryTestHarness();
        }

        protected override async Task SetupTest()
        {
            _testHarness = Services.GetRequiredService<InMemoryTestHarness>();

            await _testHarness!.Start();
            
            _newMeasurementReceivedHandler = Services.GetRequiredService<INewMeasurementReceivedHandler>();
        }

        [TearDown]
        protected override void CleanUp()
        {
            _testHarness!.Stop();
            base.CleanUp();
        }

        class TestMeasurement : IMeasurement
        {
            public decimal Temperature { get; set; }
            public int Illuminance { get; set; }
            public decimal Humidity { get; set; }
        }
    }
}