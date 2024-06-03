using Microsoft.AspNetCore.Mvc;
using RPMS.Models;

namespace RPMS.Interfaces
{
    public interface IResidentRepository
    {
        Task <IEnumerable<Resident>> GetAllResidents();

        Task<Resident>? GetResidentById(int id);

        Task<bool> Add(Resident resident);

        Task<bool> Save();
    }
}
