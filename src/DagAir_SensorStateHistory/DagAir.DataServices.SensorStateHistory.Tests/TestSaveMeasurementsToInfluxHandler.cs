using System.Threading.Tasks;
using DagAir.DataServices.SensorStateHistory.Influx.Handlers;
using DagAir.IngestionNode.Contracts;

namespace DagAir.DataServices.SensorStateHistory.Tests
{
    public class TestSaveMeasurementsToInfluxHandler : ISaveMeasurementsToInfluxHandler
    {
        public Task Handle(SaveMeasurementToInfluxDBEvent measurementsInsertedEvent)
        {
            return Task.CompletedTask;
        }
    }
}