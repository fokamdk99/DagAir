using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;

namespace DagAir.PolicyNode.MeasurementCommands
{
    public interface IEvaluatePoliciesCommand
    {
        Task Handle(IMeasurementsInsertedEvent measurementsInsertedEvent);
    }
}