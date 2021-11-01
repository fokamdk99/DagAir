using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;
using DagAir.PolicyNode.Contracts.Contracts;

namespace DagAir.PolicyNode.MeasurementCommands
{
    public interface IEvaluatePoliciesCommand
    {
        Task<PoliciesEvaluationResultEvent> Handle(MeasurementSentEvent measurementsInsertedEvent);
    }
}