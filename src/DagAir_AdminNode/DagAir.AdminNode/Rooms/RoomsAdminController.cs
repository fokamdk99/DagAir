using System.Net;
using System.Threading.Tasks;
using DagAir.AdminNode.Infrastructure.UserApi;
using DagAir.Components.ApiModels.Json;
using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.AdminNode.Rooms
{
    public class RoomsAdminController : AdminControllerBase
    {
        private readonly IRoomsHandler _roomsHandler;

        public RoomsAdminController(IRoomsHandler roomsHandler)
        {
            _roomsHandler = roomsHandler;
        }

        [HttpPost]
        [Route("rooms")]
        [ProducesResponseType(typeof(JsonApiDocument<RoomDto>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> AddNewRoom([FromBody] AddNewRoomCommand addNewRoomCommand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var newRoom = await _roomsHandler.AddNewRoom(addNewRoomCommand);
            if (newRoom == null)
            {
                string message =
                    $"Room with name {addNewRoomCommand.RoomDto.Number} already exists in the affiliate with id {addNewRoomCommand.RoomDto.AffiliateId}";
                return Conflict(new JsonApiError(HttpStatusCode.Conflict, message));
            }
            
            return Created(new JsonApiDocument<RoomDto>(newRoom));
        }
    }
}