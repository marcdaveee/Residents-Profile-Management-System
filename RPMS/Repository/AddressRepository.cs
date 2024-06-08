using Microsoft.EntityFrameworkCore;
using RPMS.Data;
using RPMS.Interfaces;
using RPMS.Models;

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
            var address = await _context.Addresses.Include(s => s.Streets).FirstOrDefaultAsync(a => a.Id == 1);
            

            return address;
            
        }

        public Task<bool> Add(Address newAddress)
        {
            throw new NotImplementedException();
        }

        

        public Task<bool> SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Address newAddress)
        {
            throw new NotImplementedException();
        }
    }
}
