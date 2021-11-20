using System;
using System.Threading.Tasks;
using DagAir.Components.MassTransit.RabbitMq.Publisher;
using DagAir.IngestionNode.Contracts;
using DagAir.IngestionNode.Influx.Handlers;
using DagAir.IngestionNode.Integrations.Sensors.DataServices;
using DagAir.IngestionNode.Measurements.Commands;
using DagAir.Sensors.Contracts.DTOs;
using Microsoft.Extensions.Logging;

namespace DagAir.IngestionNode.Measurements.Handlers
{
    public class NewMeasurementReceivedHandler : INewMeasurementReceivedHandler
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly ISaveMeasurementsToInfluxHandler _saveMeasurementsToInfluxHandler;
        private readonly ISensorsDataService _sensorDataService;
        private readonly ILogger<NewMeasurementReceivedHandler> _logger;

        public NewMeasurementReceivedHandler(IEventPublisher eventPublisher,
            ISaveMeasurementsToInfluxHandler saveMeasurementsToInfluxHandler,
            ISensorsDataService sensorDataService, ILogger<NewMeasurementReceivedHandler> logger)
        {
            _eventPublisher = eventPublisher;
            _saveMeasurementsToInfluxHandler = saveMeasurementsToInfluxHandler;
            _sensorDataService = sensorDataService;
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

            SensorDto sensor;
            try
            {
                sensor = await _sensorDataService.GetSensorById(command.SensorId);
                _logger.LogInformation("Successfully retrieved sensor data from sensors api.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting sensor from sensors api. Exception message: {ex.Message}", ex);
                return;
            }
            

            var measurementSentEvent = new MeasurementSentEvent(command.Measurement.Temperature,
                command.Measurement.Illuminance,
                command.Measurement.Humidity,
                sensor.RoomId);

            await _eventPublisher.Publish(measurementSentEvent);
            _logger.LogInformation($"MeasurementSentEvent sent from IngestionNode. " +
                                   $"Temperature: {measurementSentEvent.Temperature}, " +
                                   $"Illuminance: {measurementSentEvent.Illuminance}," +
                                   $"Humidity: {measurementSentEvent.Humidity}," +
                                   $"room id: {measurementSentEvent.RoomId}");
        }
    }
}