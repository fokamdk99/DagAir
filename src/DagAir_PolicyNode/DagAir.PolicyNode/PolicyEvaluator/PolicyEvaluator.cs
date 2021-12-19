using System;
using DagAir.IngestionNode.Contracts;
using DagAir.Policies.Contracts.DTOs;
using DagAir.PolicyNode.Contracts.Contracts;


namespace DagAir.PolicyNode.PolicyEvaluator
{
    public class PolicyEvaluator : IPolicyEvaluator
    {
        public PoliciesEvaluationResultEvent Evaluate(MeasurementSentEvent measurementSentEvent, RoomPolicyDto policy)
        {
            
            PoliciesEvaluationResultEvent evaluationResultEvent = EvaluateCurrentPolicy(policy, measurementSentEvent);

            return evaluationResultEvent;
        }

        private PoliciesEvaluationResultEvent EvaluateCurrentPolicy(RoomPolicyDto policy, MeasurementSentEvent measurementSentEvent)
        {
            PoliciesEvaluationResultEvent resultEvent = new PoliciesEvaluationResultEvent();
            resultEvent.Message = "";

            var expectedConditions = policy.ExpectedConditions;

            if (measurementSentEvent.Temperature >= expectedConditions.Temperature + expectedConditions.TemperatureMargin)
            {
                resultEvent.TemperatureStatus = EvaluatorResult.TooHigh;
                resultEvent.Message += MessageConsts.TemperatureTooHigh;
            }
            else if (measurementSentEvent.Temperature < expectedConditions.Temperature - expectedConditions.TemperatureMargin)
            {
                resultEvent.TemperatureStatus = EvaluatorResult.TooLow;
                resultEvent.Message += MessageConsts.TemperatureTooLow;
            }
            else
            {
                resultEvent.TemperatureStatus = EvaluatorResult.Normal;
            }
            
            if (measurementSentEvent.Illuminance >= expectedConditions.Illuminance + expectedConditions.IlluminanceMargin)
            {
                resultEvent.IlluminanceStatus = EvaluatorResult.TooHigh;
                resultEvent.Message += MessageConsts.IlluminanceTooHigh;
            }
            else if (measurementSentEvent.Illuminance < expectedConditions.Illuminance - expectedConditions.IlluminanceMargin)
            {
                resultEvent.IlluminanceStatus = EvaluatorResult.TooLow;
                resultEvent.Message += MessageConsts.IlluminanceTooLow;
            }
            else
            {
                resultEvent.IlluminanceStatus = EvaluatorResult.Normal;
            }
            
            if (measurementSentEvent.Humidity >= expectedConditions.Humidity + expectedConditions.HumidityMargin)
            {
                resultEvent.HumidityStatus = EvaluatorResult.TooHigh;
                resultEvent.Message += MessageConsts.HumidityTooHigh;
            }
            else if (measurementSentEvent.Humidity < expectedConditions.Humidity - expectedConditions.HumidityMargin)
            {
                resultEvent.HumidityStatus = EvaluatorResult.TooLow;
                resultEvent.Message += MessageConsts.HumidityTooLow;
            }
            else
            {
                resultEvent.HumidityStatus = EvaluatorResult.Normal;
            }

            if (String.IsNullOrEmpty(resultEvent.Message))
            {
                resultEvent.Message += MessageConsts.MeasurementsInOrder;
            }

            return resultEvent;
        }
        
    }
}