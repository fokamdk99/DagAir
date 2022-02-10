using System.Threading.Tasks;
using DagAir.Components.MassTransit.RabbitMq.Publisher;
using DagAir.DataServices.SensorState.Contracts.Commands;
using Microsoft.Extensions.Logging;

namespace DagAir.DataServices.SensorState.Measurements.Handlers
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
            await _eventPublisher.Publish(command);
            
            _logger.LogInformation($"NewMeasurementReceivedCommand sent from SensorState. " +
                                   $"Temperature: {command.Measurement.Temperature}, " +
                                   $"Illuminance: {command.Measurement.Illuminance}," +
                                   $"Humidity: {command.Measurement.Humidity}," +
                                   $"sensor name: {command.SensorName}");
        }
    }
}