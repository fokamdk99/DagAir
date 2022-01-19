using System.Threading.Tasks;

namespace DagAir.Facilities.Affiliates.Commands
{
    public interface IDeleteAffiliateHandler
    {
        Task<int> Handle(long affiliateId);
    }
}