using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.Facilities.Data.AppEntitities;

namespace DagAir.Facilities.Affiliates.Queries
{
    public interface IGetAffiliatesByOrganizationQuery
    {
        Task<ICollection<Affiliate>> Execute(long id);
    }
}