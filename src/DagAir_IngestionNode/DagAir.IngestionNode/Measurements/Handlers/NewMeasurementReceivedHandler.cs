using System;
using System.Net;
using System.Threading.Tasks;
using DagAir.Components.HttpClients;
using DagAir.Components.MassTransit.RabbitMq.Publisher;
using DagAir.IngestionNode.Contracts;
using DagAir.IngestionNode.Influx.Handlers;
using DagAir.IngestionNode.Integrations.Sensors;
using DagAir.IngestionNode.Measurements.Commands;
using Microsoft.Extensions.Logging;

namespace DagAir.IngestionNode.Measurements.Handlers
{
    public class NewMeasurementReceivedHandler : INewMeasurementReceivedHandler
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly ISaveMeasurementsToInfluxHandler _saveMeasurementsToInfluxHandler;
        private readonly IDagAirHttpClient _client;
        private readonly ILogger<NewMeasurementReceivedHandler> _logger;

        public NewMeasurementReceivedHandler(IEventPublisher eventPublisher,
            ISaveMeasurementsToInfluxHandler saveMeasurementsToInfluxHandler,
            IDagAirHttpClient client,
            ILogger<NewMeasurementReceivedHandler> logger)
        {
            _eventPublisher = eventPublisher;
            _saveMeasurementsToInfluxHandler = saveMeasurementsToInfluxHandler;
            _client = client;
            _logger = logger;
        }
        
        public async Task Handle(NewMeasurementReceivedCommand command)
        {
            await _saveMeasurementsToInfluxHandler.Handle(command);
            
            var (roomId, status) = await _client.GetAsync<long>(ApiRoutes.GetSensorBySensorId + command.SensorId);
            if (status == HttpStatusCode.NotFound)
            {
                _logger.LogError($"No room assigned to {command.SensorId} was found.");
                throw new Exception();
            }
             
            var tmp = new MeasurementSentEvent(command.Measurement.Temperature,
                command.Measurement.Illuminance,
                command.Measurement.Humidity,
                roomId);

            await _eventPublisher.Publish(tmp);
        }
    }
}