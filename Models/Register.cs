using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcECommerce.Models
{
    public class Register
    {
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [DisplayName("Username")]
        public string Username { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string RePassword { get; set; }
    }
}