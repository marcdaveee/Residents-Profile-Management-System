using Microsoft.AspNetCore.Mvc;
using RPMS.Models;
using RPMS.ViewModels;

namespace RPMS.Interfaces
{
    public interface IResidentRepository
    {
        Task <IEnumerable<Resident>> GetAllResidents(string sortBy, string searchString, int pageSize, int currentPage);

        Task<Resident>? GetResidentById(int id);

        Task<bool> Add(Resident resident);

        Task<bool> Update(Resident residentModel, EditResidentViewModel updatedResident);

        Task<bool> Delete(Resident residentModel);

        Task<bool> Save();
    }
}
