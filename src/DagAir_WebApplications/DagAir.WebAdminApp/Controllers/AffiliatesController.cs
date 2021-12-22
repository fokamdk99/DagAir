using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DagAir.AdminNode.Contracts.DTOs;
using DagAir.Facilities.Contracts.DTOs;
using DagAir.WebAdminApp.Affiliates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.WebAdminApp.Controllers
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