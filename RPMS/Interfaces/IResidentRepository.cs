using Microsoft.AspNetCore.Mvc;
using RPMS.Models;

namespace RPMS.Interfaces
{
    public interface IResidentRepository
    {
        public Task <IEnumerable<Resident>> GetAllResidents();

        public Task<Resident>? GetResidentById(int id);
    }
}
