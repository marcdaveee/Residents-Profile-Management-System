using RPMS.Models;

namespace RPMS.Interfaces
{
    public interface IStreetRepository
    {
        Task <IEnumerable<Street>> GetAll();

        Task<bool> AddStreet(Street newStreet);

        Task<bool> SaveChanges();
    }
}
