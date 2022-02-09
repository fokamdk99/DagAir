using System.Threading.Tasks;
using DagAir.DataServices.SensorState.Contracts.Commands;
using DagAir.IngestionNode.Measurements.Handlers;
using MassTransit;

namespace DagAir.IngestionNode.Consumers
{
    public class NewMeasurementReceivedCommandConsumer : IConsumer<NewMeasurementReceivedCommand>
    {
        private readonly INewMeasurementReceivedHandler _newMeasurementReceivedHandler;

        public NewMeasurementReceivedCommandConsumer(INewMeasurementReceivedHandler newMeasurementReceivedHandler)
        {
            _newMeasurementReceivedHandler = newMeasurementReceivedHandler;
        }

        public async Task Consume(ConsumeContext<NewMeasurementReceivedCommand> context)
        {
            await _newMeasurementReceivedHandler.Handle(context.Message);
        }
    }
}