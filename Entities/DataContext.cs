
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcECommerce.Entities
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext() : base("MyDbContext")
        {
            Database.SetInitializer(new DataInitializer());

        }
        

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}