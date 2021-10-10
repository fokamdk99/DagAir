namespace DagAir.PolicyNode.Contracts.Contracts
{
    public class PoliciesEvaluationResultEvent : IPoliciesEvaluationResultEvent
    {
        public int TemperatureStatus { get; set; }
        public int HumidityStatus { get; set; }
        public int IlluminanceStatus { get; set; }
        public string Message { get; set; }
    }
}