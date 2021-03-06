using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Contracts.DTOs;

namespace DagAir.AdminNode.Affiliates
{
    public interface IAffiliatesHandler
    {
        Task<List<AffiliateDto>> GetAffiliates();
        Task<AffiliateDto> GetAffiliateById(long affiliateId);
        Task<AffiliateDto> AddNewAffiliate(AddNewAffiliateCommand addNewAffiliateCommand);
    }
}