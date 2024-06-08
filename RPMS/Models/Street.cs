using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPMS.Models
{
    public class Street
    {
        
        [Key]
        public int Id { get; set; }

        public string? StreetName { get; set; } = string.Empty;

        [ForeignKey("Address")]
        public int AddressId { get; set; }

        public Address? Address { get; set; }

        public List<Resident> Residents { get; set; } = new List<Resident>();

    }
}
