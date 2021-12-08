using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DagAir.Components.ApiModels.Json;
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
        private readonly ICommandHandler<AddNewAffiliateCommand, Affiliate> _addNewRoomPolicyCommandHandler;

        public AffiliatesController(IMapper mapper,
            ICommandHandler<AddNewAffiliateCommand, Affiliate> addNewRoomPolicyCommandHandler, 
            IGetAffiliateQuery getAffiliateQuery, 
            IGetAffiliatesQuery getAffiliatesQuery)
        {
            _mapper = mapper;
            _addNewRoomPolicyCommandHandler = addNewRoomPolicyCommandHandler;
            _getAffiliateQuery = getAffiliateQuery;
            _getAffiliatesQuery = getAffiliatesQuery;
        }
        
        [HttpGet("affiliates")]
        [ProducesResponseType(typeof(JsonApiDocument<List<AffiliateDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAffiliates()
        {
            var affiliates = await _getAffiliatesQuery.Execute();

            var affiliateDtos = affiliates.Select(x => _mapper.Map<AffiliateDto>(x)).ToList();

            return Ok(new JsonApiDocument<List<AffiliateDto>>(affiliateDtos));
        }

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

        [HttpPost("affiliates")]
        [ProducesResponseType(typeof(JsonApiDocument<AffiliateDto>), (int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(JsonApiError), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateNewAffiliate(AddNewAffiliateCommand addNewAffiliateCommand)
        {
            var affiliate = await _addNewRoomPolicyCommandHandler.Handle(addNewAffiliateCommand);
            
            AffiliateDto affiliateDto = _mapper.Map<AffiliateDto>(affiliate);

            return Created(new JsonApiDocument<AffiliateDto>(affiliateDto));
        }
        
        private NotFoundObjectResult GetAffiliateNotFoundMessage(long affiliateId)
        {
            return NotFound($"No affiliate with Id: {affiliateId} has not been found");
        }
    }
}