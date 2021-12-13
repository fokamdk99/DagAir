using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using DagAir.Components.ApiModels.Json;
using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Contracts.DTOs;
using DagAir.Facilities.Data.AppEntitities;
using DagAir.Facilities.Infrastructure.UserApi;
using DagAir.Facilities.Organizations.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.Facilities.Organizations
{
    public class OrganizationsController : FacilitiesControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGetOrganizationQueryById _getOrganizationQueryById;
        private readonly IGetOrganizationsQuery _getOrganizationsQuery;
        private readonly ICommandHandler<AddNewOrganizationCommand, Organization> _commandHandler;

        public OrganizationsController(IMapper mapper, 
            IGetOrganizationQueryById getOrganizationQueryById, 
            IGetOrganizationsQuery getOrganizationsQuery,
            ICommandHandler<AddNewOrganizationCommand, Organization> commandHandler)
        {
            _mapper = mapper;
            _getOrganizationQueryById = getOrganizationQueryById;
            _getOrganizationsQuery = getOrganizationsQuery;
            _commandHandler = commandHandler;
            
        }
        
        /// <summary>
        /// Returns information about all organizations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("organizations")]
        [ProducesResponseType(typeof(JsonApiDocument<List<OrganizationDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOrganizations()
        {
            var organizations = await _getOrganizationsQuery.Execute();

            var organizationDtos = new List<OrganizationDto>();
            foreach (var organization in organizations)
            {
                organizationDtos.Add(_mapper.Map<OrganizationDto>(organization));
            }

            return Ok(new JsonApiDocument<List<OrganizationDto>>(organizationDtos));
        }

        /// <summary>
        /// Returns information about an organization with a given organizationId
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("organizations/{organizationId}")]
        [ProducesResponseType(typeof(JsonApiDocument<OrganizationDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOrganizationById(long organizationId)
        {
            var organization = await _getOrganizationQueryById.Execute(organizationId);
            if (organization == null)
            {
                return GetOrganizationNotFoundMessage(organizationId);
            }

            var organizationDto = _mapper.Map<OrganizationDto>(organization);

            return Ok(new JsonApiDocument<OrganizationDto>(organizationDto));
        }
        
        /// <summary>
        /// Create a new organization with parameters specified in addNewOrganizationCommand 
        /// </summary>
        /// <param name="addNewOrganizationCommand"></param>
        /// <returns></returns>
        [HttpPost("organizations")]
        [ProducesResponseType(typeof(JsonApiDocument<AffiliateDto>), (int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(JsonApiError), (int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateNewOrganization(AddNewOrganizationCommand addNewOrganizationCommand)
        {
            var organization = await _commandHandler.Handle(addNewOrganizationCommand);
            
            OrganizationDto organizationDto = _mapper.Map<OrganizationDto>(organization);

            return Created(new JsonApiDocument<OrganizationDto>(organizationDto));
        }

        private NotFoundObjectResult GetOrganizationNotFoundMessage(long organizationId)
        {
            return NotFound($"An organization with Id: {organizationId} has not been found");
        }
    }
}