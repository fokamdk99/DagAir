using System.Threading.Tasks;
using DagAir.IngestionNode.Measurements.Commands;

namespace DagAir.IngestionNode.Influx.Handlers
{
    public interface ISaveMeasurementsToInfluxHandler
    {
        Task Handle(NewMeasurementReceivedCommand measurementsInsertedEvent);
    }
}