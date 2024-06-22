using Microsoft.EntityFrameworkCore;
using RPMS.Data;
using RPMS.Interfaces;
using RPMS.Models;

namespace RPMS.Repository
{
    public class ResidentRepository : IResidentRepository
    {
        private readonly ApplicationDbContext _context;

        public ResidentRepository(ApplicationDbContext context)
        {
            _context = context;            
        }

        public async Task<IEnumerable<Resident>> GetAllResidents()
        {
            //var residents = await _context.Residents.Include(r => r.Address).Select(a => a.Age = DateTime.Now.Subtract(a.Birthday).).ToListAsync();
            var residents = await _context.Residents.Include(r => r.Street).ToListAsync();

            return residents;
        }

        public async Task<Resident>? GetResidentById(int id)
        {
            var resident = await _context.Residents.Include(r => r.Address).Include(r => r.Street).FirstOrDefaultAsync(r => r.Id == id);

            if(resident == null)
            {
                return null;
            }

            return resident;
        }

        public async Task<bool> Add(Resident resident)
        {
            await _context.AddAsync(resident);

            return await Save();
        }

        public async Task<bool> Save()
        {
            var result = await _context.SaveChangesAsync();
            if(result > 0)
            {
                return true;
            }
            return false;
        }
    }
}
