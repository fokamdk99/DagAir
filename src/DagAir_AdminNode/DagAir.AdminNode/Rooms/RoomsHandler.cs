using System;
using System.Net;
using System.Threading.Tasks;
using DagAir.AdminNode.Infrastructure;
using DagAir.AdminNode.Infrastructure.Facilities;
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

        public RoomsHandler(DagAirHttpClient client, IExternalServices externalServices, ILogger<RoomsHandler> logger)
        {
            _client = client;
            _externalServices = externalServices;
            _logger = logger;
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
    }
}