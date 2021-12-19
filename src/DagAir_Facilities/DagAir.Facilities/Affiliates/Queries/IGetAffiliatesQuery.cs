using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.Facilities.Data.AppEntitities;

namespace DagAir.Facilities.Affiliates.Queries
{
    public interface IGetAffiliatesQuery
    {
        Task<List<Affiliate>> Execute();
    }
}