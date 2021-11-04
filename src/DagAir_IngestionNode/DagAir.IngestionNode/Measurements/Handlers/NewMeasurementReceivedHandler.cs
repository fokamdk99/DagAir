using System.Threading.Tasks;
using DagAir.Components.MassTransit.RabbitMq.Publisher;
using DagAir.IngestionNode.Contracts;
using DagAir.IngestionNode.Influx.Handlers;
using DagAir.IngestionNode.Integrations.Sensors.DataServices;
using DagAir.IngestionNode.Measurements.Commands;

namespace DagAir.IngestionNode.Measurements.Handlers
{
    public class NewMeasurementReceivedHandler : INewMeasurementReceivedHandler
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly ISaveMeasurementsToInfluxHandler _saveMeasurementsToInfluxHandler;
        private readonly ISensorsDataService _sensorDataService;

        public NewMeasurementReceivedHandler(IEventPublisher eventPublisher,
            ISaveMeasurementsToInfluxHandler saveMeasurementsToInfluxHandler,
            ISensorsDataService sensorDataService)
        {
            _eventPublisher = eventPublisher;
            _saveMeasurementsToInfluxHandler = saveMeasurementsToInfluxHandler;
            _sensorDataService = sensorDataService;
        }
        
        public async Task Handle(NewMeasurementReceivedCommand command)
        {
            await _saveMeasurementsToInfluxHandler.Handle(command);

            var sensor = await _sensorDataService.GetSensorById(command.SensorId);

            var measurementSentEvent = new MeasurementSentEvent(command.Measurement.Temperature,
                command.Measurement.Illuminance,
                command.Measurement.Humidity,
                sensor.RoomId);

            await _eventPublisher.Publish(measurementSentEvent);
        }
    }
}