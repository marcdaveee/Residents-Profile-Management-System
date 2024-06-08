using RPMS.Models;

namespace RPMS.Interfaces
{
    public interface IAddressRepository
    {
        Task<Address>? GetAddress(); 

        Task<bool> Add(Address newAddress);

        Task<bool> Update(Address newAddress);

        Task<bool> SaveChanges();
    }
}
