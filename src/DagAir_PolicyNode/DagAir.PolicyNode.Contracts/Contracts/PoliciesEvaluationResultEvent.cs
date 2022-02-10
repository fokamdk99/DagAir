using System;
using DagAir.Policies.Contracts.DTOs;

namespace DagAir.PolicyNode.Contracts.Contracts
{
    public class PoliciesEvaluationResultEvent : IPoliciesEvaluationResultEvent
    {
        public EvaluatorResult TemperatureStatus { get; set; }
        public EvaluatorResult HumidityStatus { get; set; }
        public EvaluatorResult IlluminanceStatus { get; set; }
        public string Message { get; set; }
        public long RoomId { get; set; }
        public decimal Temperature { get; set; }
        public int Illuminance { get; set; }
        public decimal Humidity { get; set; }
        public DateTime MeasurementDate { get; set; }
        public RoomPolicyDto RoomPolicyDto { get; set; }
    }
}