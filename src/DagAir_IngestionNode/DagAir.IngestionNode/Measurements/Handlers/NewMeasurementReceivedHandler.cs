using System.Threading.Tasks;
using DagAir.Components.MassTransit.RabbitMq.Publisher;
using DagAir.IngestionNode.Contracts;
using DagAir.IngestionNode.Measurements.Commands;
using Microsoft.Extensions.Logging;

namespace DagAir.IngestionNode.Measurements.Handlers
{
    public class NewMeasurementReceivedHandler : INewMeasurementReceivedHandler
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly ILogger<NewMeasurementReceivedHandler> _logger;

        public NewMeasurementReceivedHandler(IEventPublisher eventPublisher,
            ILogger<NewMeasurementReceivedHandler> logger)
        {
            _eventPublisher = eventPublisher;
            _logger = logger;
        }
        
        public async Task Handle(NewMeasurementReceivedCommand command)
        {
            var saveMeasurementToInfluxDBEvent = new SaveMeasurementToInfluxDBEvent(command.Measurement.Temperature,
                command.Measurement.Illuminance,
                command.Measurement.Humidity,
                command.SensorName);

            await _eventPublisher.Publish(saveMeasurementToInfluxDBEvent);
            
            var measurementSentEvent = new MeasurementSentEvent(command.Measurement.Temperature,
                command.Measurement.Illuminance,
                command.Measurement.Humidity,
                command.SensorName);
            
            await _eventPublisher.Publish(measurementSentEvent);
            _logger.LogInformation($"MeasurementSentEvent sent from IngestionNode. " +
                                   $"Temperature: {measurementSentEvent.Temperature}, " +
                                   $"Illuminance: {measurementSentEvent.Illuminance}," +
                                   $"Humidity: {measurementSentEvent.Humidity}," +
                                   $"sensor name: {measurementSentEvent.SensorName}");
        }
    }
}