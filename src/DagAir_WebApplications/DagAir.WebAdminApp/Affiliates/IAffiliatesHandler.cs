using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.AdminNode.Contracts.DTOs;
using DagAir.Facilities.Contracts.DTOs;

namespace DagAir.WebAdminApp.Affiliates
{
    public interface IAffiliatesHandler
    {
        Task<List<AdminNodeAffiliateDto>> GetAffiliates();
        Task<AdminNodeAffiliateDto> GetAffiliateById(long affiliateId);
    }
}