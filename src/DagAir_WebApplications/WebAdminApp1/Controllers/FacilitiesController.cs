using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DagAir.AdminNode.Contracts.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAdminApp1.Facilities;
using WebAdminApp1.Facilities.Models;

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
            var organizationsModel = new OrganizationsModel();
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
            var organizationModel = new OrganizationModel();
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
        public async Task<IActionResult> AddNewOrganization(OrganizationModel getOrganizationModel)
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
                var organizationsModel = new OrganizationsModel();
                organizationsModel.AdminNodeOrganizationDtos = OrganizationDtos;
                
                return View("Organizations", organizationsModel);
            }

            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> DeleteOrganization(long organizationId)
        {
            var result = await _facilitiesHandler.DeleteOrganization(organizationId);
            
            await LoadAsync(User);
            var organizationsModel = new OrganizationsModel();
            organizationsModel.AdminNodeOrganizationDtos = OrganizationDtos;
            
            return View("~/Views/Facilities/Organizations.cshtml", organizationsModel);
        }
    }
}