using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcECommerce.Identity
{
    public class ApplicationUser : IdentityUser 
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? LastLoginDate { get; set; }



    }
}