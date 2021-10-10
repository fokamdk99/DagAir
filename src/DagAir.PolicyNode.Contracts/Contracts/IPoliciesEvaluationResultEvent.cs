namespace DagAir.PolicyNode.Contracts.Contracts
{
    public interface IPoliciesEvaluationResultEvent
    {
        int TemperatureStatus { get; set; }
        int HumidityStatus { get; set; }
        int IlluminanceStatus { get; set; }
        string Message { get; set; }
    }
}