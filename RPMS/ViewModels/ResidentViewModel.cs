using RPMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RPMS.ViewModels
{
    public class ResidentViewModel
    {
        public PaginatedList<Resident> PaginatedResidents { get; set; }

        public Address Address { get; set; }
        public List<Street> Streets { get; set; } = new List<Street>();

    }
}
