using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DagAir.Components.ApiModels.Json;
using DagAir.Policies.Contracts.Commands;
using DagAir.Policies.Contracts.DTOs;
using DagAir.Policies.Data.AppEntities;
using DagAir.Policies.Infrastructure.UserApi;
using DagAir.Policies.Policies.Commands;
using DagAir.Policies.RoomConditions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.Policies.RoomConditions
{
    public class ExpectedRoomConditionsController : PolicyControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGetExpectedRoomConditionsQuery _getExpectedRoomConditionsQuery;
        private readonly ICommandHandler<AddNewExpectedRoomConditionsCommand, ExpectedRoomConditions> _addNewExpectedRoomConditionsCommandHandler;

        public ExpectedRoomConditionsController(IMapper mapper, IGetExpectedRoomConditionsQuery getExpectedRoomConditionsQuery, ICommandHandler<AddNewExpectedRoomConditionsCommand, ExpectedRoomConditions> commandHandler)
        {
            _mapper = mapper;
            _getExpectedRoomConditionsQuery = getExpectedRoomConditionsQuery;
            _addNewExpectedRoomConditionsCommandHandler = commandHandler;
        }
        
        [HttpGet("expected-room-conditions/{expectedRoomConditionsId}")]
        [ProducesResponseType(typeof(JsonApiDocument<ExpectedRoomConditionsDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCurrentRoomPolicy(long expectedRoomConditionsId)
        {
            var expectedRoomConditions = await _getExpectedRoomConditionsQuery.Handle(expectedRoomConditionsId);
            
            if (expectedRoomConditions == null)
            {
                return GetCurrentRoomPolicyNotFoundMessage(expectedRoomConditionsId);
            }

            ExpectedRoomConditionsDto expectedRoomConditionsDto = _mapper.Map<ExpectedRoomConditionsDto>(expectedRoomConditions);
            
            return Ok(new JsonApiDocument<ExpectedRoomConditionsDto>(expectedRoomConditionsDto));
        }

        [HttpPost("expected-room-conditions")]
        [ProducesResponseType(typeof(JsonApiDocument<ExpectedRoomConditionsDto>), (int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(JsonApiError), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateNewExpectedRoomConditions(AddNewExpectedRoomConditionsCommand addNewExpectedRoomConditionsCommand)
        {
            var expectedRoomConditions = await _addNewExpectedRoomConditionsCommandHandler.Handle(addNewExpectedRoomConditionsCommand);
            
            ExpectedRoomConditionsDto expectedRoomConditionsDto = _mapper.Map<ExpectedRoomConditionsDto>(expectedRoomConditions);

            return Created(new JsonApiDocument<ExpectedRoomConditionsDto>(expectedRoomConditionsDto));
        }
        
        private NotFoundObjectResult GetCurrentRoomPolicyNotFoundMessage(long roomId)
        {
            return NotFound($"No current room policy for room with Id: {roomId} has not been found");
        }
    }
}