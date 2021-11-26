using System.Threading.Tasks;
using DagAir.Addresses.Data.AppEntities;

namespace DagAir.Addresses.PostalCodes.Queries
{
    public interface IGetPostalCodeQuery
    {
        Task<PostalCode> Handle(long postalCodeId);
    }
}