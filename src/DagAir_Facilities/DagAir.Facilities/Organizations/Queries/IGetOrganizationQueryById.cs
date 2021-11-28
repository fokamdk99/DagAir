using System.Threading.Tasks;
using DagAir.Facilities.Data.AppEntitities;

namespace DagAir.Facilities.Organizations.Queries
{
    public interface IGetOrganizationQueryById
    {
        Task<Organization> Execute(long id);
    }
}