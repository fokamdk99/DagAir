using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DagAir.Components.ApiModels.Json;
using DagAir.Policies.Contracts.Commands;
using DagAir.Policies.Contracts.DTOs;
using DagAir.Policies.Data.AppEntities;
using DagAir.Policies.Infrastructure.UserApi;
using DagAir.Policies.Policies.Commands;
using DagAir.Policies.Policies.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.Policies.Policies
{
    public class PoliciesController : PolicyControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGetCurrentRoomPolicyQuery _getCurrentRoomPolicyQuery;
        private readonly ICommandHandler<AddNewRoomPolicyCommand, RoomPolicy> _addNewRoomPolicyCommandHandler;

        public PoliciesController(IMapper mapper,
            IGetCurrentRoomPolicyQuery getCurrentRoomPolicyQuery, 
            ICommandHandler<AddNewRoomPolicyCommand, RoomPolicy> addNewRoomPolicyCommandHandler)
        {
            _mapper = mapper;
            _getCurrentRoomPolicyQuery = getCurrentRoomPolicyQuery;
            _addNewRoomPolicyCommandHandler = addNewRoomPolicyCommandHandler;
        }

        [HttpGet("policies/{roomId}")]
        [ProducesResponseType(typeof(JsonApiDocument<RoomPolicyDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCurrentRoomPolicy(long roomId)
        {
            var roomPolicy = await _getCurrentRoomPolicyQuery.Handle(roomId);
            
            if (roomPolicy == null)
            {
                return GetCurrentRoomPolicyNotFoundMessage(roomId);
            }

            RoomPolicyDto roomPolicyDto = _mapper.Map<RoomPolicyDto>(roomPolicy);
            
            return Ok(new JsonApiDocument<RoomPolicyDto>(roomPolicyDto));
        }

        [HttpPost("policies")]
        [ProducesResponseType(typeof(JsonApiDocument<RoomPolicyDto>), (int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(JsonApiError), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateNewRoomPolicy(AddNewRoomPolicyCommand addNewRoomPolicyCommand)
        {
            var policy = await _addNewRoomPolicyCommandHandler.Handle(addNewRoomPolicyCommand);
            
            RoomPolicyDto roomPolicyDto = _mapper.Map<RoomPolicyDto>(policy);

            return Created(new JsonApiDocument<RoomPolicyDto>(roomPolicyDto));
        }
        
        private NotFoundObjectResult GetCurrentRoomPolicyNotFoundMessage(long roomId)
        {
            return NotFound($"No current room policy for room with Id: {roomId} has not been found");
        }
    }
}