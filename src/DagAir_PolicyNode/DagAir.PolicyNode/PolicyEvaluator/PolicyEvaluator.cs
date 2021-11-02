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
                resultEvent.Message += "Current temperature level is too high. Consider turning off the heat.";
            }
            else if (measurementSentEvent.Temperature < expectedConditions.Temperature - expectedConditions.TemperatureMargin)
            {
                resultEvent.TemperatureStatus = EvaluatorResult.TooLow;
                resultEvent.Message += "Current temperature level is too low. Consider turning on the heat.";
            }
            else
            {
                resultEvent.TemperatureStatus = EvaluatorResult.Normal;
            }
            
            if (measurementSentEvent.Illuminance >= expectedConditions.Illuminance + expectedConditions.IlluminanceMargin)
            {
                resultEvent.IlluminanceStatus = EvaluatorResult.TooHigh;
                resultEvent.Message += "Current illuminance level is too high. Consider switching off the lights.";
            }
            else if (measurementSentEvent.Illuminance < expectedConditions.Illuminance - expectedConditions.IlluminanceMargin)
            {
                resultEvent.IlluminanceStatus = EvaluatorResult.TooLow;
                resultEvent.Message += "Current illuminance level is too low. Consider switching on the lights.";
            }
            else
            {
                resultEvent.IlluminanceStatus = EvaluatorResult.Normal;
            }
            
            if (measurementSentEvent.Humidity >= expectedConditions.Humidity + expectedConditions.HumidityMargin)
            {
                resultEvent.HumidityStatus = EvaluatorResult.TooHigh;
                resultEvent.Message += "Current humidity level is too high. Consider opening the windows.";
            }
            else if (measurementSentEvent.Humidity < expectedConditions.Humidity - expectedConditions.HumidityMargin)
            {
                resultEvent.HumidityStatus = EvaluatorResult.TooLow;
                resultEvent.Message += "Current humidity level is too low. Consider closing the windows.";
            }
            else
            {
                resultEvent.HumidityStatus = EvaluatorResult.Normal;
            }

            if (String.IsNullOrEmpty(resultEvent.Message))
            {
                resultEvent.Message += "Current room condition is compliant with expectations.";
            }

            return resultEvent;
        }
        
    }
}