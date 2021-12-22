using System.Threading.Tasks;
using DagAir.Addresses.Contracts.Commands;
using DagAir.Addresses.Contracts.DTOs;

namespace DagAir.AdminNode.Addresses
{
    public interface IAddressesHandler
    {
        Task<AddressDto> GetAddressById(long addressId);
        Task<AddressDto> AddNewAddress(AddNewAddressCommand addNewAddressCommand);
    }
}