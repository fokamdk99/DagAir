using System;
using System.Net;
using System.Threading.Tasks;
using DagAir.AdminNode.Contracts.Commands;
using DagAir.AdminNode.Contracts.DTOs;
using DagAir.AdminNode.Infrastructure;
using DagAir.AdminNode.Infrastructure.Facilities;
using DagAir.AdminNode.SensorStateHistory;
using DagAir.Components.HttpClients;
using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Contracts.DTOs;
using Microsoft.Extensions.Logging;

namespace DagAir.AdminNode.Rooms
{
    public class RoomsHandler : IRoomsHandler
    {
        private readonly DagAirHttpClient _client;
        private readonly IExternalServices _externalServices;
        private readonly ILogger<RoomsHandler> _logger;
        private readonly ISensorStateHistoryHandler _sensorStateHistoryHandler;

        public RoomsHandler(DagAirHttpClient client, 
            IExternalServices externalServices, 
            ILogger<RoomsHandler> logger, 
            ISensorStateHistoryHandler sensorStateHistoryHandler)
        {
            _client = client;
            _externalServices = externalServices;
            _logger = logger;
            _sensorStateHistoryHandler = sensorStateHistoryHandler;
        }

        public async Task<AdminNodeRoomDto> GetRoomByRoomId(GetRoomCommand getRoomCommand)
        {
            var path = 
                _externalServices.FacilitiesApi + FacilitiesEndpoints.GetRooms + getRoomCommand.RoomId;
            (var room, var statusCode) = await _client.GetAsync<RoomDto>(path);
            if (statusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            var pastMeasurementsDto =
                await _sensorStateHistoryHandler.GetHistoricMeasurements(getRoomCommand);

            var adminNodeRoomDto = new AdminNodeRoomDto
            {
                RoomDto = room,
                PastMeasurements = pastMeasurementsDto
            };

            return adminNodeRoomDto;
        }

        public async Task<RoomDto> AddNewRoom(AddNewRoomCommand addNewRoomCommand)
        {
            var path = _externalServices.FacilitiesApi + FacilitiesEndpoints.GetRooms;
            (var newRoom, var statusCode) = await _client.PostAsync<AddNewRoomCommand, RoomDto>(path, addNewRoomCommand);

            if (statusCode == HttpStatusCode.Conflict)
            {
                return null;
            }
            
            if (statusCode != HttpStatusCode.Created)
            {
                var message =
                    $"Error while trying to add new room. Status code: ${statusCode}. AddNewRoomCommand: {addNewRoomCommand}";
                _logger.LogError(message);
                throw new Exception(message);
            }
            
            return newRoom;
        }

        public async Task<int> DeleteRoom(long roomId)
        {
            var path = _externalServices.FacilitiesApi + FacilitiesEndpoints.GetRooms + roomId;
            var statusCode = await _client.DeleteAsync(path);

            if (statusCode == HttpStatusCode.NotFound)
            {
                return 0;
            }

            return 1;
        }
    }
}