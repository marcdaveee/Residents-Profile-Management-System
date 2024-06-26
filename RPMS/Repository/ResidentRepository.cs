using Microsoft.EntityFrameworkCore;
using RPMS.Data;
using RPMS.Interfaces;
using RPMS.Models;
using RPMS.ViewModels;

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

        public async Task<bool> Update(Resident residentModel, EditResidentViewModel updatedResident)
        {
            residentModel.Firstname = updatedResident.Firstname;
            residentModel.Lastname = updatedResident.Lastname;
            residentModel.Middlename = updatedResident.Middlename;
            residentModel.Age = updatedResident.Age;
            residentModel.Gender = updatedResident.Gender;
            residentModel.Status = updatedResident.Status;
            residentModel.Birthday = updatedResident.Birthday;
            residentModel.ContactNo = updatedResident.ContactNo;
            residentModel.Email = updatedResident.Email;            
            residentModel.StreetId  = Convert.ToInt32(updatedResident.StreetId);

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
