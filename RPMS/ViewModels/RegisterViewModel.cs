﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RPMS.ViewModels
{
    public class RegisterViewModel
    {    

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage ="The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password{ get; set; }

        [Required]
        [DataType(DataType.Password)]        
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string? ReturnURl{ get; set; }
    }
}
