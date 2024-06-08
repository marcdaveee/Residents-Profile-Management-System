using System.ComponentModel.DataAnnotations;

namespace RPMS.ViewModels
{
    public class CreateAddressVM
    {
        [Required]
        public string Street { get; set; } = string.Empty;

        [Required]
        public string Brgy { get; set; } = string.Empty;

        [Required]
        public string Municipality { get; set; } = string.Empty;

        [Required]
        public string Town { get; set; } = string.Empty;
    }
}
