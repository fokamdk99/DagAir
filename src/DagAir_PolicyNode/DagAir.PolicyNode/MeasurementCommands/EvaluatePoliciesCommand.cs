using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;
using DagAir.PolicyNode.Contracts.Contracts;
using DagAir.PolicyNode.Integrations.Facilities.DataServices;
using DagAir.PolicyNode.Integrations.Policies.DataServices;
using DagAir.PolicyNode.PolicyEvaluator;

namespace DagAir.PolicyNode.MeasurementCommands
{
    public class EvaluatePoliciesCommand : IEvaluatePoliciesCommand
    {
        private readonly IPolicyEvaluator _policyEvaluator;
        private readonly IPoliciesDataService _policiesDataService;
        private readonly IFacilitiesDataService _facilitiesDataService;

        public EvaluatePoliciesCommand(IPolicyEvaluator policyEvaluator,
            IPoliciesDataService policiesDataService, IFacilitiesDataService facilitiesDataService)
        {
            _policyEvaluator = policyEvaluator;
            _policiesDataService = policiesDataService;
            _facilitiesDataService = facilitiesDataService;
        }
        public async Task<PoliciesEvaluationResultEvent> Handle(MeasurementSentEvent measurementSentEvent)
        {
            var policy = await _policiesDataService.GetRoomPolicyByRoomId(measurementSentEvent.RoomId);
            var result = _policyEvaluator.Evaluate(measurementSentEvent, policy);

            var room = await _facilitiesDataService.GetRoomByRoomId(measurementSentEvent.RoomId);
            result.UniqueRoomId = room.UniqueRoomId;

            return result;
        }
    }
}