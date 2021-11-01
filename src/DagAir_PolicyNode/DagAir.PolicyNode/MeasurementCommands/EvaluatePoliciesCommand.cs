using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;
using DagAir.PolicyNode.Contracts.Contracts;
using DagAir.PolicyNode.Integrations.Policies.DataServices;
using DagAir.PolicyNode.PolicyEvaluator;

namespace DagAir.PolicyNode.MeasurementCommands
{
    public class EvaluatePoliciesCommand : IEvaluatePoliciesCommand
    {
        private readonly IPolicyEvaluator _policyEvaluator;
        private readonly IPoliciesDataService _policiesDataService;

        public EvaluatePoliciesCommand(IPolicyEvaluator policyEvaluator,
            IPoliciesDataService policiesDataService)
        {
            _policyEvaluator = policyEvaluator;
            _policiesDataService = policiesDataService;
        }
        public async Task<PoliciesEvaluationResultEvent> Handle(MeasurementSentEvent measurementSentEvent)
        {
            var policy = await _policiesDataService.GetRoomPolicyByRoomId(measurementSentEvent.RoomId);
            var result = _policyEvaluator.Evaluate(measurementSentEvent, policy);

            return result;
        }
    }
}