using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DagAir.Components.ApiModels.Json;
using DagAir.Facilities.Affiliates.Commands;
using DagAir.Facilities.Affiliates.Queries;
using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Contracts.DTOs;
using DagAir.Facilities.Data.AppEntitities;
using DagAir.Facilities.Infrastructure.UserApi;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.Facilities.Affiliates
{
    public class AffiliatesController : FacilitiesControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGetAffiliateQuery _getAffiliateQuery;
        private readonly IGetAffiliatesQuery _getAffiliatesQuery;
        private readonly ICommandHandler<AddNewAffiliateCommand, Affiliate> _commandHandler;
        private readonly IDeleteAffiliateHandler _deleteAffiliateHandler;

        public AffiliatesController(IMapper mapper,
            ICommandHandler<AddNewAffiliateCommand, Affiliate> addNewRoomPolicyCommandHandler, 
            IGetAffiliateQuery getAffiliateQuery, 
            IGetAffiliatesQuery getAffiliatesQuery, 
            IDeleteAffiliateHandler deleteAffiliateHandler)
        {
            _mapper = mapper;
            _commandHandler = addNewRoomPolicyCommandHandler;
            _getAffiliateQuery = getAffiliateQuery;
            _getAffiliatesQuery = getAffiliatesQuery;
            _deleteAffiliateHandler = deleteAffiliateHandler;
        }
        
        /// <summary>
        /// Returns information about all affiliates
        /// </summary>
        /// <returns></returns>
        [HttpGet("affiliates")]
        [ProducesResponseType(typeof(JsonApiDocument<List<AffiliateDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAffiliates()
        {
            var affiliates = await _getAffiliatesQuery.Execute();

            var affiliateDtos = affiliates.Select(x => _mapper.Map<AffiliateDto>(x)).ToList();

            return Ok(new JsonApiDocument<List<AffiliateDto>>(affiliateDtos));
        }

        /// <summary>
        /// Returns information about an affiliate with a given affiliateId
        /// </summary>
        /// <param name="affiliateId"></param>
        /// <returns></returns>
        [HttpGet("affiliates/{affiliateId}")]
        [ProducesResponseType(typeof(JsonApiDocument<AffiliateDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAffiliateById(long affiliateId)
        {
            var affiliate = await _getAffiliateQuery.Execute(affiliateId);
            
            if (affiliate == null)
            {
                return GetAffiliateNotFoundMessage(affiliateId);
            }

            AffiliateDto affiliateDto = _mapper.Map<AffiliateDto>(affiliate);
            
            return Ok(new JsonApiDocument<AffiliateDto>(affiliateDto));
        }

        /// <summary>
        /// Create a new affiliate with parameters specified in addNewAffiliateCommand 
        /// </summary>
        /// <param name="addNewAffiliateCommand"></param>
        /// <returns></returns>
        [HttpPost("affiliates")]
        [ProducesResponseType(typeof(JsonApiDocument<AffiliateDto>), (int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(JsonApiError), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(JsonApiError), (int) HttpStatusCode.Conflict)]
        public async Task<IActionResult> CreateNewAffiliate(AddNewAffiliateCommand addNewAffiliateCommand)
        {
            var affiliate = await _commandHandler.Handle(addNewAffiliateCommand);

            if (affiliate == null)
            {
                string message =
                    $"Affiliate with name {addNewAffiliateCommand.AffiliateDto.Name} already exists";
                return Conflict(new JsonApiError(HttpStatusCode.Conflict, message));
            }
            
            AffiliateDto affiliateDto = _mapper.Map<AffiliateDto>(affiliate);

            return Created(new JsonApiDocument<AffiliateDto>(affiliateDto));
        }
        
        [HttpDelete("affiliates/{affiliateId}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(JsonApiError), (int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteOrganization(long affiliateId)
        {
            var affectedRows = await _deleteAffiliateHandler.Handle(affiliateId);

            if (affectedRows == 0)
            {
                return GetAffiliateNotFoundMessage(affiliateId);
            }

            return NoContent();
        }
        
        private NotFoundObjectResult GetAffiliateNotFoundMessage(long affiliateId)
        {
            return NotFound($"No affiliate with Id: {affiliateId} has not been found");
        }
    }
}