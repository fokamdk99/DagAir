using System.Threading.Tasks;
using DagAir.Addresses.Data.AppEntities;

namespace DagAir.Addresses.Countries.Queries
{
    public interface IGetCountryQuery
    {
        Task<Country> Handle(long countryId);
    }
}