namespace DagAir.IngestionNode.Data.Influx
{
    public interface IInfluxConfigurationProvider
    {
        InfluxConfiguration Provide();
    }
}