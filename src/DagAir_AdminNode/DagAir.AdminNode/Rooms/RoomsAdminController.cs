using System.Net;
using System.Threading.Tasks;
using DagAir.AdminNode.Contracts.Commands;
using DagAir.AdminNode.Contracts.DTOs;
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
        [Route("rooms/get-room")]
        [ProducesResponseType(typeof(JsonApiDocument<AdminNodeRoomDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetRoomByRoomId(
            [FromBody] GetRoomCommand getRoomCommand)
        {
            var room = await _roomsHandler.GetRoomByRoomId(getRoomCommand);
            if (room == null)
            {
                string message =
                    $"No room with id {getRoomCommand.RoomId} has been found.";
                return NotFound(new JsonApiError(HttpStatusCode.Conflict, message));
            }
            
            return Ok(new JsonApiDocument<AdminNodeRoomDto>(room));
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
        
        [HttpDelete]
        [Route("rooms/{roomId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteOrganization(long roomId)
        {
            var affectedRows = await _roomsHandler.DeleteRoom(roomId);
            if (affectedRows == 0)
            {
                string message =
                    $"Room with id {roomId} has not been found";
                return NotFound(new JsonApiError(HttpStatusCode.NotFound, message));
            }
            
            return NoContent();
        }
    }
}