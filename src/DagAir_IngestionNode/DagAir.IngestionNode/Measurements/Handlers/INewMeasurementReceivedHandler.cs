using System.Threading.Tasks;
using DagAir.IngestionNode.Measurements.Commands;

namespace DagAir.IngestionNode.Measurements.Handlers
{
    public interface INewMeasurementReceivedHandler
    {
        Task Handle(NewMeasurementReceivedCommand command);
    }
}