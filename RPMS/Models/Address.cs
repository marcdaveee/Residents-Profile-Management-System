using System.ComponentModel.DataAnnotations;

namespace RPMS.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        public string Street { get; set; } = string.Empty;

        public string Brgy { get; set; } = string.Empty;

        public string Municipality { get; set; } = string.Empty;

        public string Town { get; set; } = string.Empty; 

    }
}
