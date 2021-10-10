using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;
using DagAir.PolicyNode.MeasurementCommands;
using MassTransit;

namespace DagAir.PolicyNode.Consumers
{
    public class MeasurementsInsertedEventConsumer : IConsumer<IMeasurementsInsertedEvent>
    {
        private readonly IEvaluatePoliciesCommand _evaluatePoliciesCommand;

        public MeasurementsInsertedEventConsumer(IEvaluatePoliciesCommand evaluatePoliciesCommand)
        {
            _evaluatePoliciesCommand = evaluatePoliciesCommand;
        }
        
        public async Task Consume(ConsumeContext<IMeasurementsInsertedEvent> context)
        {
            await _evaluatePoliciesCommand.Handle(context.Message);
        }
    }
}