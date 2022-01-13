using DagAir.IngestionNode.Measurements.Commands;

namespace DagAir.IngestionNode.Measurements.Handlers
{
    public interface IMeasurementDeserializer
    {
        NewMeasurementReceivedCommand DeserializeMeasurement(string message);
    }
}