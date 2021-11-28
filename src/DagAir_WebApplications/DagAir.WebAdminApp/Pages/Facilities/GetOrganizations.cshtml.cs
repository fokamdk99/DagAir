using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.Facilities.Contracts.DTOs;
using DagAir.WebAdminApp.Facilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DagAir.WebAdminApp.Pages.Facilities
{
    public partial class GetOrganizationsModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IFacilitiesHandler _facilitiesHandler;

        public GetOrganizationsModel(UserManager<IdentityUser> userManager, 
            IFacilitiesHandler facilitiesHandler)
        {
            _userManager = userManager;
            _facilitiesHandler = facilitiesHandler;
        }
        
        public List<OrganizationDto> OrganizationDtos;

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }
        
        private async Task LoadAsync(IdentityUser user)
        {
            var organizationDtos = await _facilitiesHandler.GetOrganizations();
            OrganizationDtos = organizationDtos;
        }
    }
}