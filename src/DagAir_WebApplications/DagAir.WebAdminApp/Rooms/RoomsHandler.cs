using System;
using System.Net;
using System.Threading.Tasks;
using DagAir.Components.HttpClients;
using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Contracts.DTOs;
using DagAir.WebAdminApp.Controllers;
using DagAir.WebAdminApp.Infrastructure;
using DagAir.WebAdminApp.Infrastructure.Facilities;
using Microsoft.Extensions.Logging;

namespace DagAir.WebAdminApp.Rooms
{
    public class RoomsHandler : IRoomsHandler
    {
        private readonly DagAirHttpClient _client;
        private readonly IExternalServices _externalServices;
        private readonly ILogger<RoomsHandler> _logger;

        public RoomsHandler(DagAirHttpClient client, 
            IExternalServices externalServices, 
            ILogger<RoomsHandler> logger)
        {
            _client = client;
            _externalServices = externalServices;
            _logger = logger;
        }

        public async Task<RoomDto> AddNewRoom(UniqueRoomModel uniqueRoomModel)
        {
            var roomPath = _externalServices.AdminNode + FacilitiesEndpoints.GetRooms;

            var addNewRoomCommand = new AddNewRoomCommand { RoomDto = new RoomDto() };
            addNewRoomCommand.RoomDto.Number = uniqueRoomModel.RoomDto.Number;
            addNewRoomCommand.RoomDto.Floor = uniqueRoomModel.RoomDto.Floor;
            addNewRoomCommand.RoomDto.AffiliateId = uniqueRoomModel.RoomDto.AffiliateId;
            
            (var newRoom, var roomStatusCode) = await _client.PostAsync<AddNewRoomCommand, RoomDto>(roomPath, addNewRoomCommand);

            if (roomStatusCode == HttpStatusCode.Conflict)
            {
                return null;
            }
            
            if (roomStatusCode != HttpStatusCode.Created)
            {
                var message =
                    $"Error while trying to add new room. Status code: ${roomStatusCode}. AddNewOrganizationCommand: {addNewRoomCommand}";
                _logger.LogError(message);
                throw new Exception(message);
            }

            return newRoom;
        }
    }
}