using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using DagAir.Facilities.Contracts.DTOs;
using DagAir.WebAdminApp.Facilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.WebAdminApp.Controllers
{
    [Authorize]
    public class FacilitiesController : Controller
    {
        private readonly IFacilitiesHandler _facilitiesHandler;

        public FacilitiesController(IFacilitiesHandler facilitiesHandler)
        {
            _facilitiesHandler = facilitiesHandler;
        }
        
        public List<OrganizationDto> OrganizationDtos;
        public OrganizationDto OrganizationDto;
        
        public async Task<IActionResult> Organizations()
        {
            await LoadAsync(User);
            var organizationsModel = new GetOrganizationsModel();
            organizationsModel.OrganizationDtos = OrganizationDtos;
            
            return View(organizationsModel);
        }
        
        private async Task LoadAsync(ClaimsPrincipal user)
        {
            var organizationDtos = await _facilitiesHandler.GetOrganizations();
            OrganizationDtos = organizationDtos;
        }
        
        public async Task<IActionResult> Organization(long organizationId)
        {
            await LoadAsyncOrganization(User, organizationId);
            var organizationModel = new GetOrganizationModel();
            organizationModel.OrganizationDto = OrganizationDto;
            
            return View(organizationModel);
        }
        
        private async Task LoadAsyncOrganization(ClaimsPrincipal user, long organizationId)
        {
            var organizationDto = await _facilitiesHandler.GetOrganization(organizationId);
            OrganizationDto = organizationDto;
        }
    }

    public class GetOrganizationsModel
    {
        public List<OrganizationDto> OrganizationDtos { get; set; }
    }
    public class GetOrganizationModel
    {
        public OrganizationDto OrganizationDto { get; set; }
    }
}