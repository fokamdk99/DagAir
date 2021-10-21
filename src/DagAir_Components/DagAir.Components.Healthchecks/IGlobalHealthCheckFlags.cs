namespace DagAir.Components.HealthChecks
{
    public interface IGlobalHealthCheckFlags
    {
        bool IsReady { get; set; }
    }
}