using DagAir.DataServices.SensorState.Contracts.Commands;

namespace DagAir.DataServices.SensorState.Measurements.Handlers
{
    public interface IMeasurementDeserializer
    {
        NewMeasurementReceivedCommand DeserializeMeasurement(string message);
    }
}