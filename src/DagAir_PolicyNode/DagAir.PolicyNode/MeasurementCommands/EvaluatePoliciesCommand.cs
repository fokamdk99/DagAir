using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;
using DagAir.Components.MassTransit.RabbitMq.Publisher;
using DagAir.PolicyNode.Integrations.Policies.DataServices;
using DagAir.PolicyNode.PolicyEvaluator;

namespace DagAir.PolicyNode.MeasurementCommands
{
    public class EvaluatePoliciesCommand : IEvaluatePoliciesCommand
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly IPolicyEvaluator _policyEvaluator;
        private readonly IPoliciesDataService _policiesDataService;

        public EvaluatePoliciesCommand(IEventPublisher eventPublisher,
            IPolicyEvaluator policyEvaluator,
            IPoliciesDataService policiesDataService)
        {
            _eventPublisher = eventPublisher;
            _policyEvaluator = policyEvaluator;
            _policiesDataService = policiesDataService;
        }
        public async Task Handle(MeasurementSentEvent measurementSentEvent)
        {
            var policy = await _policiesDataService.GetRoomPolicyByRoomId(measurementSentEvent.RoomId);
            var result = await _policyEvaluator.Evaluate(measurementSentEvent, policy);
            
            //TODO: send result to rabbitmq 
            //await _eventPublisher.Publish<MeasurementsInsertedEvent>(); //publish message 
        }
    }
}