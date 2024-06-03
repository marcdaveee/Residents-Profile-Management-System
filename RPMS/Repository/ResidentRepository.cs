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
            var residents = await _context.Residents.Include(r => r.Address).ToListAsync();

            return residents;
        }

        public async Task<Resident>? GetResidentById(int id)
        {
            var resident = await _context.Residents.Include(r => r.Address).FirstOrDefaultAsync(r => r.Id == id);

            if(resident == null)
            {
                return null;
            }

            return resident;
        }
    }
}
