using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;
using DagAir.Policies.Contracts.DTOs;
using DagAir.PolicyNode.Contracts.Contracts;


namespace DagAir.PolicyNode.PolicyEvaluator
{
    public class PolicyEvaluator : IPolicyEvaluator
    {
        public PoliciesEvaluationResultEvent Evaluate(MeasurementSentEvent measurementSentEvent, RoomPolicyDto policy)
        {
            
            PoliciesEvaluationResultEvent evaluationResultEvent = EvaluateCurrentPolicy(policy, measurementSentEvent);

            return evaluationResultEvent;
        }

        private PoliciesEvaluationResultEvent EvaluateCurrentPolicy(RoomPolicyDto policy, MeasurementSentEvent measurementSentEvent)
        {
            //TODO: provide real implementation
            PoliciesEvaluationResultEvent resultEvent = new PoliciesEvaluationResultEvent();

            return resultEvent;
        }
        
    }
}