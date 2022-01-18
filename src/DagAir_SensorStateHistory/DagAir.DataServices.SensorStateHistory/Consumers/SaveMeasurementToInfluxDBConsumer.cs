using System.Threading.Tasks;
using DagAir.DataServices.SensorStateHistory.Influx.Handlers;
using DagAir.IngestionNode.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace DagAir.DataServices.SensorStateHistory.Consumers
{
    public class SaveMeasurementToInfluxDBConsumer : IConsumer<SaveMeasurementToInfluxDBEvent>
    {
        private readonly ISaveMeasurementsToInfluxHandler _saveMeasurementsToInfluxHandler;
        private readonly ILogger<SaveMeasurementToInfluxDBConsumer> _logger;

        public SaveMeasurementToInfluxDBConsumer(ISaveMeasurementsToInfluxHandler saveMeasurementsToInfluxHandler, 
            ILogger<SaveMeasurementToInfluxDBConsumer> logger)
        {
            _saveMeasurementsToInfluxHandler = saveMeasurementsToInfluxHandler;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SaveMeasurementToInfluxDBEvent> context)
        {
            var saveMeasurementToInfluxDBEvent = context.Message;
            await _saveMeasurementsToInfluxHandler.Handle(saveMeasurementToInfluxDBEvent);
            _logger.LogInformation($"Measurement saved by sensor state history data service. " +
                                   $"Temperature: {saveMeasurementToInfluxDBEvent.Temperature}, " +
                                   $"Illuminance: {saveMeasurementToInfluxDBEvent.Illuminance}," +
                                   $"Humidity: {saveMeasurementToInfluxDBEvent.Humidity}," +
                                   $"sensor name: {saveMeasurementToInfluxDBEvent.SensorName}");
        }
    }
}