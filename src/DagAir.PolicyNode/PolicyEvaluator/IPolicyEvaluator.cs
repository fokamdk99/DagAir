using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;
using DagAir.PolicyNode.Contracts.Contracts;

namespace DagAir.PolicyNode.PolicyEvaluator
{
    public interface IPolicyEvaluator
    {
        Task<PoliciesEvaluationResultEvent> Evaluate(IMeasurementsInsertedEvent measurementsInsertedEvent);
    }
}