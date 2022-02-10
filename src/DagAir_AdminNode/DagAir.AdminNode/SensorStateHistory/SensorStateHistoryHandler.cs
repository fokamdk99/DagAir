using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DagAir.AdminNode.Contracts.Commands;
using DagAir.AdminNode.Infrastructure;
using DagAir.AdminNode.Infrastructure.Policies;
using DagAir.AdminNode.Infrastructure.SensorStateHistory;
using DagAir.AdminNode.Sensors;
using DagAir.Components.HttpClients;
using DagAir.DataServices.SensorStateHistory.Contracts.Commands;
using DagAir.DataServices.SensorStateHistory.Contracts.Contracts;
using DagAir.Policies.Contracts.Commands;
using DagAir.Policies.Contracts.DTOs;
using Microsoft.Extensions.Logging;

namespace DagAir.AdminNode.SensorStateHistory
{
    public class SensorStateHistoryHandler : ISensorStateHistoryHandler
    {
        private readonly DagAirHttpClient _client;
        private readonly IExternalServices _externalServices;
        private readonly ILogger<SensorStateHistoryHandler> _logger;
        private readonly ISensorsHandler _sensorsHandler;

        public SensorStateHistoryHandler(DagAirHttpClient client, 
            IExternalServices externalServices, 
            ILogger<SensorStateHistoryHandler> logger, 
            ISensorsHandler sensorsHandler)
        {
            _client = client;
            _externalServices = externalServices;
            _logger = logger;
            _sensorsHandler = sensorsHandler;
        }

        public async Task<PastMeasurementsDto> GetHistoricMeasurements(GetRoomCommand getRoomCommand)
        {
            var sensorDto = await _sensorsHandler.GetSensorByRoomId(getRoomCommand.RoomId);

            if (sensorDto == null)
            {
                return new PastMeasurementsDto();
            }
            
            var path = _externalServices.SensorStateHistory + SensorStateHistoryEndpoints.GetHistoricMeasurements;

            var getMeasurementsFromSensorCommand = new GetMeasurementsFromSensorCommand
            {
                NumberOfRecords = getRoomCommand.NumberOfRecords,
                SensorName = sensorDto.SensorName,
                RoomId = getRoomCommand.RoomId
            };
            
            (var historicMeasurements, var statusCode) = 
                await _client.PostAsync<GetMeasurementsFromSensorCommand, List<HistoricMeasurement>>
                    (path, getMeasurementsFromSensorCommand);

            if (statusCode != HttpStatusCode.OK)
            {
                var message =
                    $"Error while trying to retrieve historic measurements. Status code: ${statusCode}. " +
                    $"GetMeasurementsFromSensorCommand: {getRoomCommand}";
                _logger.LogError(message);
                throw new Exception(message);
            }
            
            var pastMeasurementsPath = _externalServices.PoliciesDataService + PoliciesEndpoints.PastMeasurements;
            
            var getPastPoliciesCommand = new GetPastPoliciesCommand
            {
                RoomId = getRoomCommand.RoomId,
                HistoricMeasurements = historicMeasurements
            };
            
            (var pastMeasurements, var pastMeasurementsStatusCode) = 
                await _client.PostAsync<GetPastPoliciesCommand, PastMeasurementsDto>
                    (pastMeasurementsPath, getPastPoliciesCommand);
            
            if (pastMeasurementsStatusCode != HttpStatusCode.OK)
            {
                var message =
                    $"Error while trying to retrieve past policies. Status code: ${statusCode}. " +
                    $"GetPastPoliciesCommand: {getRoomCommand}";
                _logger.LogError(message);
                throw new Exception(message);
            }

            return pastMeasurements;
        }
    }
}