using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;
using DagAir.Components.MassTransit.RabbitMq.Publisher;
using DagAir.PolicyNode.PolicyEvaluator;

namespace DagAir.PolicyNode.MeasurementCommands
{
    public class EvaluatePoliciesCommand : IEvaluatePoliciesCommand
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly IPolicyEvaluator _policyEvaluator;

        public EvaluatePoliciesCommand(IEventPublisher eventPublisher,
            IPolicyEvaluator policyEvaluator)
        {
            _eventPublisher = eventPublisher;
            _policyEvaluator = policyEvaluator;
        }
        public async Task Handle(MeasurementSentEvent measurementsInsertedEvent)
        {
            
            //await _eventPublisher.Publish<MeasurementsInsertedEvent>(); //publish message 
        }
    }
}