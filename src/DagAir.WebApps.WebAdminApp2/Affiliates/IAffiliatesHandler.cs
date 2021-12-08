using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.Facilities.Contracts.DTOs;

namespace DagAir.WebApps.WebAdminApp2.Affiliates
{
    public interface IAffiliatesHandler
    {
        Task<List<AffiliateDto>> GetAffiliates();
    }
}