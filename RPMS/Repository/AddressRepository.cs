using Microsoft.EntityFrameworkCore;
using RPMS.Data;
using RPMS.Interfaces;
using RPMS.Models;
using RPMS.ViewModels;

namespace RPMS.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Address>? GetAddress()
        {
            var address = await _context.Addresses.Include(s => s.Streets).OrderBy(a => a.Brgy).FirstOrDefaultAsync();
            
            return address;
            
        }

        public async Task<bool> Add(Address addressModel)
        {
            
            await _context.AddAsync(addressModel);
            return await SaveChanges();
        }


        public async Task<bool> Update(Address addressToUpdate, EditAddressViewModel updatedAddress)
        {
            addressToUpdate.Brgy = updatedAddress.Brgy;
            addressToUpdate.Municipality = updatedAddress.Municipality; 
            addressToUpdate.Town = updatedAddress.Town;

            return await SaveChanges();
        }


        public async Task<bool> SaveChanges()
        {
            var result = await _context.SaveChangesAsync();
            if(result >= 0)
            {
                return true;
            }
            return false;
        }
    }
}
