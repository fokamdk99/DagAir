using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DagAir.AdminNode.Contracts.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAdminApp1.Facilities;

namespace WebAdminApp1.Controllers
{
    [Authorize]
    public class FacilitiesController : Controller
    {
        private readonly IFacilitiesHandler _facilitiesHandler;

        public FacilitiesController(IFacilitiesHandler facilitiesHandler)
        {
            _facilitiesHandler = facilitiesHandler;
        }
        
        public List<AdminNodeOrganizationDto> OrganizationDtos;
        public AdminNodeOrganizationDto OrganizationDto;
        
        public async Task<IActionResult> Organizations()
        {
            await LoadAsync(User);
            var organizationsModel = new GetOrganizationsModel();
            organizationsModel.AdminNodeOrganizationDtos = OrganizationDtos;
            
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
            organizationModel.AdminNodeOrganizationDto = OrganizationDto;
            
            return View(organizationModel);
        }
        
        private async Task LoadAsyncOrganization(ClaimsPrincipal user, long organizationId)
        {
            var organizationDto = await _facilitiesHandler.GetOrganization(organizationId);
            OrganizationDto = organizationDto;
        }

        [HttpGet]
        public async Task<IActionResult> AddNewOrganization()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> AddNewOrganization(GetOrganizationModel getOrganizationModel)
        {
            if (ModelState.IsValid)
            {
                var newOrganization = await _facilitiesHandler.AddNewOrganization(getOrganizationModel);

                if (newOrganization == null)
                {
                    string message =
                        $"Organization with name {getOrganizationModel.AdminNodeOrganizationDto.OrganizationDto.Name} already exists. Please choose other name";
                    ModelState.AddModelError(string.Empty, message);
                    return View();
                }
                
                await LoadAsync(User);
                var organizationsModel = new GetOrganizationsModel();
                organizationsModel.AdminNodeOrganizationDtos = OrganizationDtos;
                
                return View("Organizations", organizationsModel);
            }

            return View();
        }
    }

    public class GetOrganizationsModel
    {
        public List<AdminNodeOrganizationDto> AdminNodeOrganizationDtos { get; set; }
    }
    public class GetOrganizationModel
    {
        public AdminNodeOrganizationDto AdminNodeOrganizationDto { get; set; }
    }
}