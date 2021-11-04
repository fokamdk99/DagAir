using System.Threading.Tasks;
using DagAir.IngestionNode.Influx.Handlers;
using DagAir.IngestionNode.Measurements.Commands;

namespace DagAir.IngestionNode.Tests
{
    public class TestSaveMeasurementsToInfluxHandler : ISaveMeasurementsToInfluxHandler
    {
        public Task Handle(NewMeasurementReceivedCommand measurementsInsertedEvent)
        {
            return Task.CompletedTask;
        }
    }
}