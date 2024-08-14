using Microsoft.AspNetCore.Mvc;
using RPMS.Models;
using RPMS.ViewModels;

namespace RPMS.Interfaces
{
    public interface IResidentRepository
    {
        Task <PaginatedList<Resident>> GetAllResidents(string sortBy, string searchString, string streetId, int pageIndex, int pageSize);

        Task<Resident>? GetResidentById(int id);

        Task<bool> Add(Resident resident);

        Task<bool> Update(Resident residentModel, EditResidentViewModel updatedResident);

        Task<bool> Delete(Resident residentModel);

        Task<bool> Exists(string lastname, string firstname, int? streetId);

        Task<bool> Save();
    }
}
