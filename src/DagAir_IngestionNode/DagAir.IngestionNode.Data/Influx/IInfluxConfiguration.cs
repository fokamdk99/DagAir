namespace DagAir.IngestionNode.Data.Influx
{
    public interface IInfluxConfiguration
    {
        string Token { get; }
        string Org { get; }
        string OrgId { get; }
        string BucketName { get; }
        string Url { get; }
        int Retention { get; }
        
    }
}