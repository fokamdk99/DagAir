using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;

namespace DagAir.IngestionNode.InfluxCommands
{
    public interface ISaveMeasurementsToInfluxCommand
    {
        Task Handle(IMeasurementsInsertedEvent measurementsInsertedEvent);
    }
}