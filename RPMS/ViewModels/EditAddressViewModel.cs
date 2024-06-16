using RPMS.Models;
using System.ComponentModel.DataAnnotations;

namespace RPMS.ViewModels
{
    public class EditAddressViewModel
    {

        [Required]
        public string Brgy { get; set; } = string.Empty;

        [Required]
        public string Municipality { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter the Town Name.")]
        [MinLength(3, ErrorMessage = "Town name must be minimum of 3 characters.")] 
        public string Town { get; set; } = string.Empty;

    }
}
