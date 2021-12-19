using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.Facilities.Contracts.DTOs;

namespace DagAir.WebAdminApp.Affiliates
{
    public interface IAffiliatesHandler
    {
        Task<List<AffiliateDto>> GetAffiliates();
        Task<AffiliateDto> GetAffiliateById(long affiliateId);
    }
}