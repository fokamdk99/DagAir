namespace DagAir.IngestionNode.Contracts
{
    public interface IMeasurementsInsertedEvent
    {
        IMeasurement Measurement { get; }
        string SensorId { get; }
    }
}
