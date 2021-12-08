using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using DagAir.Facilities.Contracts.DTOs;
using DagAir.WebApps.WebAdminApp2.Facilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.WebApps.WebAdminApp2.Controllers
{
    public class FacilitiesController : Controller
    {
        private readonly IFacilitiesHandler _facilitiesHandler;

        public FacilitiesController(IFacilitiesHandler facilitiesHandler)
        {
            _facilitiesHandler = facilitiesHandler;
        }
        
        public List<OrganizationDto> OrganizationDtos;

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
    }

    public class GetOrganizationsModel
    {
        public List<OrganizationDto> OrganizationDtos { get; set; }
    }
    
}