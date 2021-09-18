namespace DagAir.IngestionNode.Contracts
{
    public interface IMeasurement
    {
        float Temperature { get; }
        float Illuminance { get; } //light intensity
        float Humidity { get; }
    }
}