using RPMS.Models;
using RPMS.ViewModels;

namespace RPMS.Interfaces
{
    public interface IAddressRepository
    {
        Task<Address>? GetAddress(); 

        Task<bool> Add(Address addressModel);

        Task<bool> Update(Address addressToUpdate, EditAddressViewModel updatedAddress);

        Task<bool> SaveChanges();
    }
}
