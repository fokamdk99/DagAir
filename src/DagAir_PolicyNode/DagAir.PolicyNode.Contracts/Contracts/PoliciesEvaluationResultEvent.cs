namespace DagAir.PolicyNode.Contracts.Contracts
{
    public class PoliciesEvaluationResultEvent : IPoliciesEvaluationResultEvent
    {
        public EvaluatorResult TemperatureStatus { get; set; }
        public EvaluatorResult HumidityStatus { get; set; }
        public EvaluatorResult IlluminanceStatus { get; set; }
        public string Message { get; set; }
    }
}