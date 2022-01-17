namespace DagAir.Components.Influx
{
    public interface IInfluxConfiguration
    {
        string Token { get; }
        string Org { get; }
        string OrgId { get; set; }
        string BucketName { get; }
        string Url { get; }
        int Retention { get; }
        
    }
}