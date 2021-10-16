using System;
using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;
using DagAir.PolicyNode.MeasurementCommands;
using MassTransit;

namespace DagAir.PolicyNode.Consumers
{
    public class MeasurementSentEventConsumer : IConsumer<MeasurementSentEvent>
    {
        private readonly IEvaluatePoliciesCommand _evaluatePoliciesCommand;

        public MeasurementSentEventConsumer(IEvaluatePoliciesCommand evaluatePoliciesCommand)
        {
            _evaluatePoliciesCommand = evaluatePoliciesCommand;
        }
        
        public async Task Consume(ConsumeContext<MeasurementSentEvent> context)
        {
            Console.WriteLine($"Received imeasurementsinsertedevent! temperature: {context.Message.Temperature}");
            await _evaluatePoliciesCommand.Handle(context.Message);
        }
    }
}