using RPMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RPMS.ViewModels
{
    public class CreateResidentViewModel
    {
        [Required]
        public string Firstname { get; set; } = string.Empty;

        [Required]
        public string Lastname { get; set; } = string.Empty;

        [Required]
        public string Middlename { get; set; } = string.Empty;
        
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = string.Empty;

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        [MinLength(11, ErrorMessage="Must be 11-digit number")]
        public string? ContactNo { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string? Email { get; set; } = string.Empty;

        public CreateAddressViewModel? Address { get; set; }
    }
}
