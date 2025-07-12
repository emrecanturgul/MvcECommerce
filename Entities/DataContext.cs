
using Microsoft.AspNet.Identity.EntityFramework;
using MvcECommerce.Identity;
using MvcECommerce.Models;
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
        
        }
        
         
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductModel> productModels { get; set;  }
        public DbSet<OrderLine> OrderLines { get; set; }

        
    }
}