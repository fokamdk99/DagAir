using System.Threading.Tasks;
using DagAir.IngestionNode.Contracts;
using DagAir.PolicyNode.Contracts.Contracts;
using DagAir.PolicyNode.Integrations.Facilities.DataServices;
using DagAir.PolicyNode.Integrations.Policies.DataServices;
using DagAir.PolicyNode.Integrations.Sensors.DataServices;
using DagAir.PolicyNode.PolicyEvaluator;

namespace DagAir.PolicyNode.MeasurementCommands
{
    public class EvaluatePoliciesCommand : IEvaluatePoliciesCommand
    {
        private readonly IPolicyEvaluator _policyEvaluator;
        private readonly IPoliciesDataService _policiesDataService;
        private readonly IFacilitiesDataService _facilitiesDataService;
        private readonly ISensorsDataService _sensorsDataService;

        public EvaluatePoliciesCommand(IPolicyEvaluator policyEvaluator,
            IPoliciesDataService policiesDataService, 
            IFacilitiesDataService facilitiesDataService, 
            ISensorsDataService sensorsDataService)
        {
            _policyEvaluator = policyEvaluator;
            _policiesDataService = policiesDataService;
            _facilitiesDataService = facilitiesDataService;
            _sensorsDataService = sensorsDataService;
        }
        public async Task<PoliciesEvaluationResultEvent> Handle(MeasurementSentEvent measurementSentEvent)
        {
            var sensorDto = await _sensorsDataService.GetSensorBySensorName(measurementSentEvent.SensorName);
            var roomDto = await _facilitiesDataService.GetRoomByRoomId(sensorDto.RoomId);
            var policyDto = await _policiesDataService.GetRoomPolicyByRoomId(roomDto.Id);
            
            var result = _policyEvaluator.Evaluate(measurementSentEvent, policyDto);
            
            result.RoomId = roomDto.Id;
            result.RoomPolicyDto = policyDto;
            result.Temperature = measurementSentEvent.Temperature;
            result.Illuminance = measurementSentEvent.Illuminance;
            result.Humidity = measurementSentEvent.Humidity;

            return result;
        }
    }
}