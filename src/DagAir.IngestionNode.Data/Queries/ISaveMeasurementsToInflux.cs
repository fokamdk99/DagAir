using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;

namespace DagAir.IngestionNode.Data.Queries
{
    public interface ISaveMeasurementsToInflux
    {
        Task Handle(IMeasurementsInsertedEvent measurementsInsertedEvent);
    }
}