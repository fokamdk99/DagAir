using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DagAir.Addresses.Contracts.DTOs;
using DagAir.AdminNode.Contracts.DTOs;
using DagAir.Facilities.Contracts.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAdminApp1.Affiliates;
using WebAdminApp1.Affiliates.Models;
using WebAdminApp1.Facilities;
using WebAdminApp1.Facilities.Models;

namespace WebAdminApp1.Controllers
{
    [Authorize]
    public class AffiliatesController : Controller
    {
        private readonly IAffiliatesHandler _affiliatesHandler;
        private readonly IFacilitiesHandler _facilitiesHandler;

        public List<AdminNodeAffiliateDto> AffiliateDtos;
        public AdminNodeAffiliateDto AffiliateDto;

        public AffiliatesController(IAffiliatesHandler affiliatesHandler, 
            IFacilitiesHandler facilitiesHandler)
        {
            _affiliatesHandler = affiliatesHandler;
            _facilitiesHandler = facilitiesHandler;
        }

        public async Task<IActionResult> Affiliates()
        {
            await LoadAsync(User);
            var affiliatesModel = new AffiliatesModel();
            affiliatesModel.AdminNodeAffiliateDtos = AffiliateDtos;

            return View(affiliatesModel);
        }

        private async Task LoadAsync(ClaimsPrincipal user)
        {
            var affiliateDtos = await _affiliatesHandler.GetAffiliates();
            AffiliateDtos = affiliateDtos;
        }

        public async Task<IActionResult> Affiliate(long affiliateId)
        {
            await LoadAsyncAffiliate(User, affiliateId);
            var affiliateModel = new AffiliateModel();
            affiliateModel.AdminNodeAffiliateDto = AffiliateDto;

            return View(affiliateModel);
        }

        private async Task LoadAsyncAffiliate(ClaimsPrincipal user, long affiliateId)
        {
            var affiliateDto = await _affiliatesHandler.GetAffiliateById(affiliateId);
            AffiliateDto = affiliateDto;
        }

        [HttpGet]
        public async Task<IActionResult> AddNewAffiliate(long organizationId)
        {
            var getAffiliateModel = new AffiliateModel
            {
                AdminNodeAffiliateDto = new AdminNodeAffiliateDto
                {
                    AffiliateDto = new AffiliateDto(),
                    AddressDto = new AddressDto()
                }
            };
            getAffiliateModel.AdminNodeAffiliateDto.AffiliateDto.OrganizationId = organizationId;
            return View(getAffiliateModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewAffiliate(AffiliateModel getAffiliateModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var newAffiliate = await _affiliatesHandler.AddNewAffiliate(getAffiliateModel);

            if (newAffiliate == null)
            {
                string message =
                    $"Organization with name {getAffiliateModel.AdminNodeAffiliateDto.AffiliateDto.Name} already exists. Please choose other name";
                ModelState.AddModelError(string.Empty, message);

                return View(getAffiliateModel);
            }

            await LoadAsync(User);
            var organizationsModel = new AffiliatesModel();
            organizationsModel.AdminNodeAffiliateDtos = AffiliateDtos;

            return View("Affiliates", organizationsModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAffiliate(long organizationId, long affiliateId)
        {
            var result = await _affiliatesHandler.DeleteAffiliate(affiliateId);

            var organizationDto = await _facilitiesHandler.GetOrganization(organizationId);
            var organizationModel = new OrganizationModel();
            organizationModel.AdminNodeOrganizationDto = organizationDto;

            return View("~/Views/Facilities/Organization.cshtml", organizationModel);
        }
    }
}