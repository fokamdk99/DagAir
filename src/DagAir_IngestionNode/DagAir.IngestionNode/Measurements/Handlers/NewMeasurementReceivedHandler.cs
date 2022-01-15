using System;
using System.Threading.Tasks;
using DagAir.Components.MassTransit.RabbitMq.Publisher;
using DagAir.IngestionNode.Contracts;
using DagAir.IngestionNode.Influx.Handlers;
using DagAir.IngestionNode.Measurements.Commands;
using Microsoft.Extensions.Logging;

namespace DagAir.IngestionNode.Measurements.Handlers
{
    public class NewMeasurementReceivedHandler : INewMeasurementReceivedHandler
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly ISaveMeasurementsToInfluxHandler _saveMeasurementsToInfluxHandler;
        private readonly ILogger<NewMeasurementReceivedHandler> _logger;

        public NewMeasurementReceivedHandler(IEventPublisher eventPublisher,
            ISaveMeasurementsToInfluxHandler saveMeasurementsToInfluxHandler, 
            ILogger<NewMeasurementReceivedHandler> logger)
        {
            _eventPublisher = eventPublisher;
            _saveMeasurementsToInfluxHandler = saveMeasurementsToInfluxHandler;
            _logger = logger;
        }
        
        public async Task Handle(NewMeasurementReceivedCommand command)
        {
            try
            {
                await _saveMeasurementsToInfluxHandler.Handle(command);
                _logger.LogInformation("Successfully written measurement to influxdb.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error writing measurement to influxdb. Exception message: {ex.Message}", ex);
                return;
            }

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