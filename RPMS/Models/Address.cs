﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RPMS.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        
        public string Brgy { get; set; } = string.Empty;

        public string Municipality { get; set; } = string.Empty;

        public string Town { get; set; } = string.Empty;

        public List<Street> Streets { get; set; } = new List<Street>();

    }
}
