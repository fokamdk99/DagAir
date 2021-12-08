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
    }

    public class GetAffiliatesModel
    {
        public List<AffiliateDto> AffiliateDtos { get; set; }
    }
}