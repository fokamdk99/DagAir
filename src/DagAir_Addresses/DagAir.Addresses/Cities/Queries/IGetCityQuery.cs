using System.Threading.Tasks;
using DagAir.Addresses.Data.AppEntities;

namespace DagAir.Addresses.Cities.Queries
{
    public interface IGetCityQuery
    {
        Task<City> Handle(long cityId);
    }
}