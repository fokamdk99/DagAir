using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DagAir.Components.ApiModels.Json;
using DagAir.Policies.Contracts.DTOs;
using DagAir.Policies.Infrastructure.UserApi;
using DagAir.Policies.Policies.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.Policies.Policies
{
    public class PoliciesController : PolicyControllerBase
    {
        private readonly IMapper _mapper;
        private IGetCurrentRoomPolicyQuery _getCurrentRoomPolicyQuery;

        public PoliciesController(IMapper mapper,
            IGetCurrentRoomPolicyQuery getCurrentRoomPolicyQuery)
        {
            _mapper = mapper;
            _getCurrentRoomPolicyQuery = getCurrentRoomPolicyQuery;
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
        
        private NotFoundObjectResult GetCurrentRoomPolicyNotFoundMessage(long roomId)
        {
            return NotFound($"No current room policy for room with Id: {roomId} has not been found");
        }
    }
}