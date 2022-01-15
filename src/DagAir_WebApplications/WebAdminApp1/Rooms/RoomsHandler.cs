using System;
using System.Net;
using System.Threading.Tasks;
using DagAir.AdminNode.Contracts.Commands;
using DagAir.AdminNode.Contracts.DTOs;
using DagAir.Components.HttpClients;
using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Contracts.DTOs;
using Microsoft.Extensions.Logging;
using WebAdminApp1.Infrastructure;
using WebAdminApp1.Infrastructure.Facilities;
using WebAdminApp1.Infrastructure.SensorStateHistory;
using WebAdminApp1.Rooms.Models;

namespace WebAdminApp1.Rooms
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

        public async Task<RoomDto> AddNewRoom(RoomModel roomModel)
        {
            var roomPath = _externalServices.AdminNode + FacilitiesEndpoints.GetRooms;

            var addNewRoomCommand = new AddNewRoomCommand { RoomDto = new RoomDto() };
            addNewRoomCommand.RoomDto.Number = roomModel.AdminNodeRoomDto.RoomDto.Number;
            addNewRoomCommand.RoomDto.Floor = roomModel.AdminNodeRoomDto.RoomDto.Floor;
            addNewRoomCommand.RoomDto.AffiliateId = roomModel.AdminNodeRoomDto.RoomDto.AffiliateId;
            
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

        public async Task<AdminNodeRoomDto> GetRoom(long roomId)
        {
            var getRoomPath = _externalServices.AdminNode + RoomEndpoints.GetRoom;
            var getRoomCommand = new GetRoomCommand()
            {
                RoomId = roomId,
                NumberOfRecords = 5
            };
            
            (var adminNodeRoomDto, var statusCode) = await _client.PostAsync<GetRoomCommand, AdminNodeRoomDto>(getRoomPath, getRoomCommand);

            if (statusCode != HttpStatusCode.OK)
            {
                var message =
                    $"Error while trying to get room data. Status code: ${statusCode}. RoomId: {roomId}";
                _logger.LogError(message);
                throw new Exception(message);
            }

            return adminNodeRoomDto;
        }
    }
}