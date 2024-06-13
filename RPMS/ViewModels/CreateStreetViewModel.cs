using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RPMS.ViewModels
{
    public class CreateStreetViewModel
    {

        [Required (ErrorMessage = "Please enter the street name.")]
        [MinLength(3, ErrorMessage = "Street name must be minimum of 3 characters.")]        
        public string? StreetName { get; set; }

        public int AddressId { get; set; }
    }
}
