using System.Collections.Generic;
using System.Threading.Tasks;
using DagAir.Facilities.Data.AppEntitities;

namespace DagAir.Facilities.Organizations.Queries
{
    public interface IGetOrganizationsQuery
    {
        Task<ICollection<Organization>> Execute();
    }
}