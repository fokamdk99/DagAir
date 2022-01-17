namespace DagAir.DataServices.SensorStateHistory.Contracts.Contracts
{
    public class IMeasurement
    {
        decimal Temperature { get; }
        int Illuminance { get; }
        decimal Humidity { get; }
    }
}