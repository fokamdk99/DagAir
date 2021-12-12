using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DagAir.Facilities.Contracts.DTOs;
using DagAir.WebAdminApp.Affiliates;
using Microsoft.AspNetCore.Mvc;

namespace DagAir.WebAdminApp.Controllers
{
    public class AffiliatesController : Controller
    {
        private readonly IAffiliatesHandler _affiliatesHandler;
        
        public List<AffiliateDto> AffiliateDtos;
        public AffiliateDto AffiliateDto;

        public AffiliatesController(IAffiliatesHandler affiliatesHandler)
        {
            _affiliatesHandler = affiliatesHandler;
        }

        public async Task<IActionResult> Affiliates()
        {
            await LoadAsync(User);
            var affiliatesModel = new GetAffiliatesModel();
            affiliatesModel.AffiliateDtos = AffiliateDtos;
            
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
            affiliateModel.AffiliateDto = AffiliateDto;
            
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
        public List<AffiliateDto> AffiliateDtos { get; set; }
    }
    
    public class GetAffiliateModel
    {
        public AffiliateDto AffiliateDto { get; set; }
    }
}