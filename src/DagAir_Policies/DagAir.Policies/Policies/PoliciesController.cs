using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DagAir.Components.ApiModels.Json;
using DagAir.DataServices.SensorStateHistory.Contracts.Contracts;
using DagAir.Policies.Contracts.Commands;
using DagAir.Policies.Contracts.DTOs;
using DagAir.Policies.Data.AppEntities;
using DagAir.Policies.Infrastructure.UserApi;
using DagAir.Policies.Policies.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.Policies.Policies
{
    public class PoliciesController : PolicyControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGetRoomPolicyQuery _getRoomPolicyQuery;
        private readonly ICommandHandler<AddNewRoomPolicyCommand, RoomPolicy> _addNewRoomPolicyCommandHandler;
        private readonly ICommandHandler<GetPastPoliciesCommand, PastMeasurements> _pastPoliciesCommandHandler;

        public PoliciesController(IMapper mapper,
            IGetRoomPolicyQuery getCurrentRoomPolicyQuery, 
            ICommandHandler<AddNewRoomPolicyCommand, RoomPolicy> addNewRoomPolicyCommandHandler, 
            ICommandHandler<GetPastPoliciesCommand, PastMeasurements> pastPoliciesCommandHandler)
        {
            _mapper = mapper;
            _getRoomPolicyQuery = getCurrentRoomPolicyQuery;
            _addNewRoomPolicyCommandHandler = addNewRoomPolicyCommandHandler;
            _pastPoliciesCommandHandler = pastPoliciesCommandHandler;
        }

        /// <summary>
        /// Returns information about a policy currently in force in a room with a given roomId
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [HttpGet("policies/{roomId}")]
        [ProducesResponseType(typeof(JsonApiDocument<RoomPolicyDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCurrentRoomPolicy(long roomId)
        {
            var roomPolicy = await _getRoomPolicyQuery.Handle(roomId, DateTime.Now);
            
            if (roomPolicy == null)
            {
                return GetCurrentRoomPolicyNotFoundMessage(roomId);
            }

            RoomPolicyDto roomPolicyDto = _mapper.Map<RoomPolicyDto>(roomPolicy);
            
            return Ok(new JsonApiDocument<RoomPolicyDto>(roomPolicyDto));
        }

        /// <summary>
        /// Create a new policy with parameters specified in addNewRoomPolicyCommand
        /// </summary>
        /// <param name="addNewRoomPolicyCommand"></param>
        /// <returns></returns>
        [HttpPost("policies")]
        [ProducesResponseType(typeof(JsonApiDocument<RoomPolicyDto>), (int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(JsonApiError), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateNewRoomPolicy(AddNewRoomPolicyCommand addNewRoomPolicyCommand)
        {
            var policy = await _addNewRoomPolicyCommandHandler.Handle(addNewRoomPolicyCommand);
            
            RoomPolicyDto roomPolicyDto = _mapper.Map<RoomPolicyDto>(policy);

            return Created(new JsonApiDocument<RoomPolicyDto>(roomPolicyDto));
        }
        
        [HttpPost("policies/past-policies")]
        [ProducesResponseType(typeof(JsonApiDocument<PastMeasurementsDto>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPastPolicies([FromBody] GetPastPoliciesCommand getPastPoliciesCommand)
        {
            var pastMeasurements = await _pastPoliciesCommandHandler.Handle(getPastPoliciesCommand);

            PastMeasurementsDto pastMeasurementsDto = new PastMeasurementsDto();
            pastMeasurementsDto.Measurements = new List<Tuple<HistoricMeasurement, RoomPolicyDto>>();
            pastMeasurementsDto.Measurements.AddRange(
                pastMeasurements.Measurements.Select(x =>
                {
                    var roomPolicyDto = _mapper.Map<RoomPolicyDto>(x.Item2);
                    var tuple = new Tuple<HistoricMeasurement, RoomPolicyDto>(x.Item1, roomPolicyDto);
                    return tuple;
                })
            );

            return Ok(new JsonApiDocument<PastMeasurementsDto>(pastMeasurementsDto));
        }
        
        private NotFoundObjectResult GetCurrentRoomPolicyNotFoundMessage(long roomId)
        {
            return NotFound($"No current room policy for room with Id: {roomId} has not been found");
        }
    }
}