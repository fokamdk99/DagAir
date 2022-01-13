using DagAir.IngestionNode.Measurements.Handlers;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DagAir.IngestionNode.Tests.Handlers
{
    public class MeasurementDeserializerTests : MassTransitIntegrationTest
    {
        [Test]
        public void WhenNewMeasurementSentFromSensor_ShouldDeserializeMeasurement()
        {
            string measurement = "23.950000;438;45.480000;968376";

            var measurementDeserializer = Services.GetRequiredService<IMeasurementDeserializer>();
            
            var result = measurementDeserializer.DeserializeMeasurement(measurement);
            
            Assert.AreEqual(23.95, result.Measurement.Temperature);
            Assert.AreEqual(438, result.Measurement.Illuminance);
            Assert.AreEqual(45.48, result.Measurement.Humidity);
        }

        protected override void AddOverrides(IServiceCollection services)
        {
            
        }
    }
}