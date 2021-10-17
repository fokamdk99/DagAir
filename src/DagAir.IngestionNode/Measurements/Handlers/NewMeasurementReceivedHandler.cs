using System.Threading.Tasks;
using DagAir.Components.MassTransit.RabbitMq.Publisher;
using DagAir.IngestionNode.Contracts;
using DagAir.IngestionNode.Influx.Handlers;
using DagAir.IngestionNode.Measurements.Commands;

namespace DagAir.IngestionNode.Measurements.Handlers
{
    public class NewMeasurementReceivedHandler : INewMeasurementReceivedHandler
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly ISaveMeasurementsToInfluxHandler _saveMeasurementsToInfluxHandler;

        public NewMeasurementReceivedHandler(IEventPublisher eventPublisher,
            ISaveMeasurementsToInfluxHandler saveMeasurementsToInfluxHandler)
        {
            _eventPublisher = eventPublisher;
            _saveMeasurementsToInfluxHandler = saveMeasurementsToInfluxHandler;
        }
        
        public async Task Handle(NewMeasurementReceivedCommand command)
        {
            await _saveMeasurementsToInfluxHandler.Handle(command);
            //Get roomId 
            var tmp = new MeasurementSentEvent(command.Measurement.Temperature,
                command.Measurement.Illuminance,
                command.Measurement.Humidity,
                133);
                
            await _eventPublisher.Publish(tmp);
        }
    }
}