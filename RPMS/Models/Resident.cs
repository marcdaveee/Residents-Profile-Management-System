using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPMS.Models
{
    public class Resident
    {
        [Key]
        public int Id { get; set; }

        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;

        public string Middlename { get; set; } = string.Empty;

        public int Age { get; set; }

        public DateTime Birthday { get; set; }

        public string? ContactNo { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; } = string.Empty;

        [ForeignKey("Address")]
        public int AddressId { get; set; }

        public Address Address { get; set; }

    }
}
