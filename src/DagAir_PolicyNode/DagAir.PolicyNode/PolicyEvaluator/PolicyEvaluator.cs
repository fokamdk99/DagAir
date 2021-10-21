using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;
using DagAir.PolicyNode.Contracts.Contracts;
using DagAir.PolicyNode.Data.AppEntities;

namespace DagAir.PolicyNode.PolicyEvaluator
{
    public class PolicyEvaluator : IPolicyEvaluator
    {
        
        public async Task<PoliciesEvaluationResultEvent> Evaluate(IMeasurementsInsertedEvent measurementsInsertedEvent)
        {
            var policies = new List<RoomPolicy>();
            RoomPolicy currentRoomPolicy = GetCurrentPolicy(policies);
            PoliciesEvaluationResultEvent evaluationResultEvent = EvaluateCurrentPolicy(currentRoomPolicy, measurementsInsertedEvent);

            return evaluationResultEvent;
        }

        private RoomPolicy GetCurrentPolicy(List<RoomPolicy> policies)
        {
            RoomPolicy currentRoomPolicy;
            currentRoomPolicy = GetCurrentPolicyFromGroup(policies.Where(x => x.Category.Name == "personal").ToList());
            if (currentRoomPolicy != null)
            {
                return currentRoomPolicy;
            }
            currentRoomPolicy = GetCurrentPolicyFromGroup(policies.Where(x => x.Category.Name == "departmental").ToList());
            if (currentRoomPolicy != null)
            {
                return currentRoomPolicy;
            }
            currentRoomPolicy = GetCurrentPolicyFromGroup(policies.Where(x => x.Category.Name == "organizational").ToList());
            if (currentRoomPolicy != null)
            {
                return currentRoomPolicy;
            }
            
            currentRoomPolicy = GetCurrentPolicyFromGroup(policies.Where(x => x.Category.Name == "general").ToList());

            return currentRoomPolicy;
        }

        private RoomPolicy GetCurrentPolicyFromGroup(List<RoomPolicy> policies)
        {
            return policies.SingleOrDefault(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now);
        }

        private PoliciesEvaluationResultEvent EvaluateCurrentPolicy(RoomPolicy policy, IMeasurementsInsertedEvent measurementsInsertedEvent)
        {
            //TODO: trzymaj tekst i inne parametry w bazie danych, dzieki temu zwiekszy sie konfigurowalnosc aplikacji
            PoliciesEvaluationResultEvent resultEvent = new PoliciesEvaluationResultEvent();

            resultEvent.HumidityStatus = 0;
            resultEvent.TemperatureStatus = 0;
            resultEvent.IlluminanceStatus = 0;
            
            if (policy.ExpectedConditions.Humidity - measurementsInsertedEvent.Measurement.Humidity < -3)
            {
                resultEvent.HumidityStatus = -1;
                resultEvent.Message += "Current humidity level is too low. Consider buying an air humidifier.";
            }
            else if (policy.ExpectedConditions.Humidity - measurementsInsertedEvent.Measurement.Humidity >= 3)
            {
                resultEvent.HumidityStatus = 1;
                resultEvent.Message += "Current humidity level is too high. Consider turning on the radiator.";
            }
            
            if (policy.ExpectedConditions.Temperature - measurementsInsertedEvent.Measurement.Temperature < -3)
            {
                resultEvent.TemperatureStatus = -1;
                resultEvent.Message += "Current temperature is too low. Consider turning on the radiatior.";
            }
            else if (policy.ExpectedConditions.Temperature - measurementsInsertedEvent.Measurement.Temperature >= 3)
            {
                resultEvent.TemperatureStatus = 1;
                resultEvent.Message += "Current temperature is too high. Consider turning on the radiator.";
            }
            
            if (policy.ExpectedConditions.Illuminance - measurementsInsertedEvent.Measurement.Illuminance < -3)
            {
                resultEvent.IlluminanceStatus = -1;
                resultEvent.Message += "Current illuminance level is too low. Consider turning the lights on.";
            }
            else if (policy.ExpectedConditions.Illuminance - measurementsInsertedEvent.Measurement.Illuminance >= 3)
            {
                resultEvent.IlluminanceStatus = 1;
                resultEvent.Message += "Current humidity level is too high. Consider turning the lights off.";
            }

            if (string.IsNullOrEmpty(resultEvent.Message))
            {
                resultEvent.Message += "The conditions in the room are excellent.";
            }

            return resultEvent;
        }
        
    }
}