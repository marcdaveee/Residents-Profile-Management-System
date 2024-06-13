using Microsoft.EntityFrameworkCore;
using RPMS.Data;
using RPMS.Interfaces;
using RPMS.Models;
using RPMS.ViewModels;

namespace RPMS.Repository
{
    public class StreetRepository : IStreetRepository
    {
        private readonly ApplicationDbContext _context;

        public StreetRepository(ApplicationDbContext context)

        {
            _context = context;
        }

        public async Task<IEnumerable<Street>> GetAll()
        {
            var streets = await _context.Streets.ToListAsync();

            return streets;
        }

        public async Task<Street> GetById(int id)
        {
            var street = await _context.Streets.FirstOrDefaultAsync(s => s.Id == id);

            return street;
        }

        public async Task<bool> AddStreet(Street newStreet)
        {
            await _context.Streets.AddAsync(newStreet);
            return await SaveChanges();
        }


        public async Task<bool> Edit(Street streetToUpdate, EditStreetViewModel newStreetData)
        {

            streetToUpdate.StreetName = newStreetData.StreetName;

            return await SaveChanges();
        }

        public async Task<bool> Delete(Street streetToDelete)
        {
            _context.Streets.Remove(streetToDelete);
            return await SaveChanges();
        }

        public async Task<bool> SaveChanges()
        {
            var result = await _context.SaveChangesAsync();
            if(result > 0)
            {
                return true;
            }
            else { return false; }
        }

        
    }
}
