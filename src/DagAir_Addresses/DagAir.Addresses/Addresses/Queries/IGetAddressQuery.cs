using System.Threading.Tasks;
using DagAir.Addresses.Data.AppEntities;

namespace DagAir.Addresses.Addresses.Queries
{
    public interface IGetAddressQuery
    {
        Task<Address> Handle(long addressId);
    }
}