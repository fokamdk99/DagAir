using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DagAir.Components.ApiModels.Json;
using DagAir.Facilities.Contracts.DTOs;
using DagAir.Facilities.Infrastructure;
using DagAir.Facilities.Rooms.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.Facilities.Rooms
{
    public class RoomController : FacilitiesControllerBase
    {
        private readonly IGetCurrentRoomQuery _getCurrentRoom;
        private readonly IMapper _mapper;

        public RoomController(IGetCurrentRoomQuery getCurrentRoomQuery, IMapper mapper)
        {
            _getCurrentRoom = getCurrentRoomQuery;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns information about a room with a given roomId
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("rooms/{roomId}")]
        [ProducesResponseType(typeof(JsonApiDocument<RoomDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCurrentRoomById(long roomId)
        {
            var currentRoom = await _getCurrentRoom.Execute(roomId);
            if (currentRoom == null)
            {
                return GetCurrentRoomNotFoundMessage(roomId);
            }

            var currentRoomDto = _mapper.Map<RoomDto>(currentRoom);

            return Ok(new JsonApiDocument<RoomDto>(currentRoomDto));
        }

        private NotFoundObjectResult GetCurrentRoomNotFoundMessage(long id)
        {
            return NotFound($"A current room with Id: {id} has not been found");
        }
    }
}