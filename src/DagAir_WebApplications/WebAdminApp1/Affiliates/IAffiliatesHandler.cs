using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.AdminNode.Contracts.DTOs;
using DagAir.Facilities.Contracts.DTOs;
using WebAdminApp1.Controllers;

namespace WebAdminApp1.Affiliates
{
    public interface IAffiliatesHandler
    {
        Task<List<AdminNodeAffiliateDto>> GetAffiliates();
        Task<AdminNodeAffiliateDto> GetAffiliateById(long affiliateId);
        Task<AffiliateDto> AddNewAffiliate(GetAffiliateModel getAffiliateModel);
    }
}