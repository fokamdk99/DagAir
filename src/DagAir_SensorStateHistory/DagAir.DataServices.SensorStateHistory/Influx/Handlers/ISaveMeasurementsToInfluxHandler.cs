using System.Threading.Tasks;
using DagAir.DataServices.SensorStateHistory.Influx.Commands;
using DagAir.IngestionNode.Contracts;

namespace DagAir.DataServices.SensorStateHistory.Influx.Handlers
{
    public interface ISaveMeasurementsToInfluxHandler
    {
        Task Handle(SaveMeasurementToInfluxDBEvent measurementsInsertedEvent);
    }
}