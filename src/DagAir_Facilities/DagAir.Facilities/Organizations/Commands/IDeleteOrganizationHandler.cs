using System.Threading.Tasks;

namespace DagAir.Facilities.Organizations.Commands
{
    public interface IDeleteOrganizationHandler
    {
        Task<int> Handle(long organizationId);
    }
}