namespace DagAir.PolicyNode.Contracts.Contracts
{
    public interface IPoliciesEvaluationResultEvent
    {
        EvaluatorResult TemperatureStatus { get; set; }
        EvaluatorResult HumidityStatus { get; set; }
        EvaluatorResult IlluminanceStatus { get; set; }
        string Message { get; set; }
    }
}