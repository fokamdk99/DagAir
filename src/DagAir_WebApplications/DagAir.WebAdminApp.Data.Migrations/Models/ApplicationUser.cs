using Microsoft.AspNetCore.Identity;

namespace DagAir.WebAdminApp.Data.Migrations.Models
{
    public class ApplicationUser : IdentityUser
    {
        public long? OrganizationId { get; set; }
    }
}