using System.Net;
using System.Threading.Tasks;
using DagAir.Components.ApiModels.Json;
using DagAir.QueryNode.Infrastructure;
using DagAir.QueryNode.Rooms.Models;
using DagAir.QueryNode.Rooms.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.QueryNode.Rooms
{
    public class RoomController : QueryNodeControllerBase
    {
        private readonly IGetCurrentRoom _getCurrentRoom;

        public RoomController(IGetCurrentRoom getCurrentRoom)
        {
            _getCurrentRoom = getCurrentRoom;
        }

        [HttpGet]
        [Route("query/rooms/{id}")]
        [ProducesResponseType(typeof(JsonApiDocument<CurrentRoomReadModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCurrentRoom(long id)
        {
            var currentRoom = await _getCurrentRoom.Execute(id);
            if (currentRoom == null)
            {
                return GetCurrentRoomNotFoundMessage(id);
            }

            return Ok(new JsonApiDocument<CurrentRoomReadModel>(currentRoom));
        }

        private NotFoundObjectResult GetCurrentRoomNotFoundMessage(long id)
        {
            return NotFound($"A current room with Id: {id} has not been found");
        }
    }
}