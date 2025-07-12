using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcECommerce.Models
{
    public class Login
    {
        [Required]
        [DisplayName("Username")]
        public string Username { get; set; }

        //[Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be null")]
        
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}