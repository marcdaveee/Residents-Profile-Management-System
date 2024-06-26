﻿using Microsoft.EntityFrameworkCore;
using RPMS.Data;
using RPMS.Interfaces;
using RPMS.Models;
using RPMS.ViewModels;
using System.Linq;

namespace RPMS.Repository
{
    public class ResidentRepository : IResidentRepository
    {
        private readonly ApplicationDbContext _context;

        public ResidentRepository(ApplicationDbContext context)
        {
            _context = context;            
        }

        public async Task<IEnumerable<Resident>> GetAllResidents(string sortBy, string searchString)
        {
            var residents = _context.Residents.Include(r => r.Street).AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                residents = residents.Where(r => r.Firstname.Contains(searchString) || r.Lastname.Contains(searchString));
            }

            switch (sortBy)
            {
                case "name_desc":
                    residents = residents.OrderByDescending(r => r.Lastname);
                    break;

                case "street":
                    residents = residents.OrderBy(r => r.Street.StreetName);
                    break;

                case "street_desc":
                    residents = residents.OrderByDescending(r => r.Street.StreetName);
                    break;

                default:
                    residents = residents.OrderBy(r => r.Lastname);
                    break;
            }
                        
             
            return await residents.AsNoTracking().ToListAsync();
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

        public async Task<bool> Delete(Resident residentModel)
        {
            _context.Residents.Remove(residentModel);

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
