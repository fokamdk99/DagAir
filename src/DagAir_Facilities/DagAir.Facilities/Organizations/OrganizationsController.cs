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
        private readonly IGetOrganizationQuery _getOrganizationQuery;
        private readonly ICommandHandler<AddNewOrganizationCommand, Organization> _commandHandler;

        public OrganizationsController(IMapper mapper, IGetOrganizationQuery getOrganizationQuery, ICommandHandler<AddNewOrganizationCommand, Organization> commandHandler)
        {
            _mapper = mapper;
            _getOrganizationQuery = getOrganizationQuery;
            _commandHandler = commandHandler;
        }

        [HttpGet]
        [Route("organizations/{organizationId}")]
        [ProducesResponseType(typeof(JsonApiDocument<OrganizationDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonApiError), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOrganizationById(long organizationId)
        {
            var organization = await _getOrganizationQuery.Execute(organizationId);
            if (organization == null)
            {
                return GetOrganizationNotFoundMessage(organizationId);
            }

            var organizationDto = _mapper.Map<OrganizationDto>(organization);

            return Ok(new JsonApiDocument<OrganizationDto>(organizationDto));
        }
        
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