using System.ComponentModel.DataAnnotations;

namespace RPMS.ViewModels
{
    public class CreateStreetVM
    {
        [Required]
        public string StreetName { get; set; } = string.Empty;
    }
}
