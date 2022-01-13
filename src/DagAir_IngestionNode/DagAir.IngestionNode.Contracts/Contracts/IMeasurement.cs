namespace DagAir.IngestionNode.Contracts
{
    public interface IMeasurement
    {
        decimal Temperature { get; }
        int Illuminance { get; } //light intensity
        decimal Humidity { get; }
    }
}