﻿using System.ComponentModel.DataAnnotations;

namespace RunGroupWebApp.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name="Email Address")]
        [Required(ErrorMessage ="Email address required.")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }    
    }
}
