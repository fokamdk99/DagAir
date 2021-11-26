using System.Threading.Tasks;
using DagAir.Facilities.Data.AppEntitities;

namespace DagAir.Facilities.Affiliates.Queries
{
    public interface IGetAffiliateQuery
    {
        Task<Affiliate> Execute(long id);
    }
}