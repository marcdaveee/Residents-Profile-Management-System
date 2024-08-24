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

        public string? PhotoPath { get; set; } = string.Empty;

        public string? Suffix { get; set; } = string.Empty;

        public string? Middlename { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Gender { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        public string? ContactNo { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; } = string.Empty;

        [ForeignKey("Address")]
        public int AddressId { get; set; }

        public Address? Address { get; set; }
        
        [ForeignKey("Street")]
        public int StreetId { get; set; }

        public Street? Street { get; set; }


        [DataType(DataType.MultilineText)]
        public string? Remarks { get; set; } = string.Empty;
    }
}
