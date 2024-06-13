using RPMS.Models;
using RPMS.ViewModels;

namespace RPMS.Interfaces
{
    public interface IStreetRepository
    {
        Task <IEnumerable<Street>> GetAll();

        Task<Street> GetById(int id);

        Task<bool> AddStreet(Street newStreet);

        Task<bool> Edit(Street streetToUpdate, EditStreetViewModel newStreet);

        Task<bool> Delete(Street streetToDelete);

        Task<bool> SaveChanges();
    }
}
