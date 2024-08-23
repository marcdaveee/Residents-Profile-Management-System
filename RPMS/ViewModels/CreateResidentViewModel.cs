using RPMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RPMS.ViewModels
{
    public class CreateResidentViewModel
    {
        [Required]
        public string Firstname { get; set; } = string.Empty;

        [Required]
        public string Lastname { get; set; } = string.Empty;
        
        public string? Middlename { get; set; } = string.Empty;

        public string? Suffix {  get; set; } = string.Empty;

        public IFormFile? Photo { get; set; }

        public int Age { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = string.Empty;
        
        [DataType(DataType.Date, ErrorMessage = "The value must be a valid date")]
        public DateTime? Birthday { get; set; }
        
        [MinLength(11, ErrorMessage="Must be 11-digit number")]
        public string? ContactNo { get; set; } = string.Empty;
        
        [EmailAddress]
        public string? Email { get; set; } = string.Empty;

        //public Address? Address { get; set; }
        [Required]
        [ForeignKey("Address")]
        public int AddressId { get; set; }

        //public Street? Street { get; set; }

        [Required (ErrorMessage = "Street is required")]
        [ForeignKey("Street")]
        public int? StreetId { get; set; }

        public Address? Address { get; set; }

        //List of Street Entity
        public IEnumerable<SelectListItem>? Streets{ get; set; }
        
    }
}
