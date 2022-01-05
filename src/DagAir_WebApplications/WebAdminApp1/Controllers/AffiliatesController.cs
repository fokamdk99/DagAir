using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DagAir.Addresses.Contracts.DTOs;
using DagAir.AdminNode.Contracts.DTOs;
using DagAir.Facilities.Contracts.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAdminApp1.Affiliates;

namespace WebAdminApp1.Controllers
{
    [Authorize]
    public class AffiliatesController : Controller
    {
        private readonly IAffiliatesHandler _affiliatesHandler;
        
        public List<AdminNodeAffiliateDto> AffiliateDtos;
        public AdminNodeAffiliateDto AffiliateDto;

        public AffiliatesController(IAffiliatesHandler affiliatesHandler)
        {
            _affiliatesHandler = affiliatesHandler;
        }

        public async Task<IActionResult> Affiliates()
        {
            await LoadAsync(User);
            var affiliatesModel = new GetAffiliatesModel();
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
            var affiliateModel = new GetAffiliateModel();
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
            var getAffiliateModel = new GetAffiliateModel
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
        public async Task<IActionResult> AddNewAffiliate(GetAffiliateModel getAffiliateModel)
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
            var organizationsModel = new GetAffiliatesModel();
            organizationsModel.AdminNodeAffiliateDtos = AffiliateDtos;
                
            return View("Affiliates", organizationsModel);
        }
    }

    public class GetAffiliatesModel
    {
        public List<AdminNodeAffiliateDto> AdminNodeAffiliateDtos { get; set; }
    }
    
    public class GetAffiliateModel
    {
        public AdminNodeAffiliateDto AdminNodeAffiliateDto { get; set; }
    }
}