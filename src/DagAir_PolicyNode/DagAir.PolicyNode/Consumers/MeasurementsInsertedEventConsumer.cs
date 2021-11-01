using System;
using System.Threading.Tasks;
using DagAir.Components.MassTransit.RabbitMq.Publisher;
using DagAir.IngestionNode.Contracts;
using DagAir.PolicyNode.MeasurementCommands;
using MassTransit;

namespace DagAir.PolicyNode.Consumers
{
    public class MeasurementSentEventConsumer : IConsumer<MeasurementSentEvent>
    {
        private readonly IEvaluatePoliciesCommand _evaluatePoliciesCommand;
        private readonly IEventPublisher _eventPublisher;

        public MeasurementSentEventConsumer(IEvaluatePoliciesCommand evaluatePoliciesCommand,
            IEventPublisher eventPublisher)
        {
            _evaluatePoliciesCommand = evaluatePoliciesCommand;
            _eventPublisher = eventPublisher;
        }
        
        public async Task Consume(ConsumeContext<MeasurementSentEvent> context)
        {
            Console.WriteLine($"Received imeasurementsinsertedevent! temperature: {context.Message.Temperature}");
            var result = await _evaluatePoliciesCommand.Handle(context.Message);

            await _eventPublisher.Publish(result);
        }
    }
}