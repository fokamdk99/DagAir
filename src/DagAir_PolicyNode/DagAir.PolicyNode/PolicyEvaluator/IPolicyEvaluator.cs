using DagAir.IngestionNode.Contracts;
using DagAir.Policies.Contracts.DTOs;
using DagAir.PolicyNode.Contracts.Contracts;

namespace DagAir.PolicyNode.PolicyEvaluator
{
    public interface IPolicyEvaluator
    {
        PoliciesEvaluationResultEvent Evaluate(MeasurementSentEvent measurementsInsertedEvent, RoomPolicyDto policy);
    }
}