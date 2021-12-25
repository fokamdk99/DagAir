using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DagAir.Components.ApiModels.Json;
using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Contracts.DTOs;
using DagAir.Facilities.Data.AppEntitities;
using DagAir.Facilities.Infrastructure;
using DagAir.Facilities.Rooms.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.Facilities.Rooms
{
    public class RoomController : FacilitiesControllerBase
    {
        private readonly IGetCurrentRoomQuery _getCurrentRoom;
        private readonly IMapper _mapper;
        private readonly ICommandHandler<AddNewRoomCommand, Room> _commandHandler;
        
        public RoomController(IGetCurrentRoomQuery getCurrentRoomQuery, 
            IMapper mapper, 
            ICommandHandler<AddNewRoomCommand, Room> commandHandler)
        {
            _getCurrentRoom = getCurrentRoomQuery;
            _mapper = mapper;
            _commandHandler = commandHandler;
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
        
        /// <summary>
        /// Create a new room with parameters specified in addNewRoomCommand 
        /// </summary>
        /// <param name="addNewRoomCommand"></param>
        /// <returns></returns>
        [HttpPost("rooms")]
        [ProducesResponseType(typeof(JsonApiDocument<RoomDto>), (int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(JsonApiError), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(JsonApiError), (int) HttpStatusCode.Conflict)]
        public async Task<IActionResult> CreateNewRoom(AddNewRoomCommand addNewRoomCommand)
        {
            var room = await _commandHandler.Handle(addNewRoomCommand);

            if (room == null)
            {
                string message =
                    $"Room with name {addNewRoomCommand.RoomDto.Number} already exists in affiliate with id {addNewRoomCommand.RoomDto.AffiliateId}";
                return Conflict(new JsonApiError(HttpStatusCode.Conflict, message));
            }
            
            RoomDto roomDto = _mapper.Map<RoomDto>(room);

            return Created(new JsonApiDocument<RoomDto>(roomDto));
        }

        private NotFoundObjectResult GetCurrentRoomNotFoundMessage(long id)
        {
            return NotFound($"A current room with Id: {id} has not been found");
        }
    }
}