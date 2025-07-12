using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MvcECommerce.Entities;
using MvcECommerce.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MvcECommerce.Initializers
{
    public class CombinedInitializer : CreateDatabaseIfNotExists<DataContext>
    {
        protected override void Seed(DataContext context)
        {
           
            var roleStore = new RoleStore<ApplicationRole>(context);
            var roleManager = new RoleManager<ApplicationRole>(roleStore);

            if (!context.Roles.Any(i => i.Name == "superadmin"))
                roleManager.Create(new ApplicationRole { Name = "superadmin", Description = "admin role" });

            if (!context.Roles.Any(i => i.Name == "user"))
                roleManager.Create(new ApplicationRole { Name = "user", Description = "user role" });

            if (!context.Roles.Any(i => i.Name == "admin"))
                roleManager.Create(new ApplicationRole { Name = "admin", Description = "admin role" });

            if (!context.Users.Any(i => i.UserName == "emrecan"))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user = new ApplicationUser
                {
                    Name = "emre",
                    Surname = "can",
                    UserName = "emrecan",
                    Email = "emrecan@gmail.com"
                };

                var result = userManager.Create(user, "Emrecan123!");

                if (result.Succeeded)
                    userManager.AddToRole(user.Id, "superadmin");
            }

            
            var categories = new List<Category>
            {
                new Category { Name = "All Models", Description = "All Tesla Cars" },
                new Category { Name = "Sedan", Description = "Tesla Sedan Series" },
                new Category { Name = "SUV", Description = "Tesla SUV Series" },
                new Category { Name = "Performance", Description = "High-Performance Teslas" },
                new Category { Name = "Cyber", Description = "Cyber Series" },
                new Category { Name = "Supercar", Description = "Ultra-high speed Tesla" }
            };

            foreach (var category in categories)
                context.Categories.Add(category);

            context.SaveChanges();


            var products = new List<Product>
            {
                new Product {
                    Name = "Model S",
                    Description = "Luxury electric sedan. 0-100 km/h in 2.1s. 830km range.",
                    Image = "Model-S_75.jpg",
                    Price = 79999,
                    Stock = 5,
                    IsApproved = true,
                    IsHome = true,
                    CategoryId = categories[1].Id
                },
                new Product {
                    Name = "Model 3",
                    Description = "Affordable, long-range sedan. 0-100 in 3.3s.",
                    Price = 39999,
                    Image = "Model3-91-EN.jpg",
                    Stock = 8,
                    IsApproved = true,
                    IsHome = true,
                    CategoryId = categories[1].Id
                },
                new Product {
                    Name = "Model X",
                    Description = "SUV with falcon doors, 7 seats. 0-100 in 2.6s.",
                    Image = "Model-X_95.jpg",

                    Price = 89999,
                    Stock = 4,
                    IsApproved = true,
                    IsHome = true,
                    CategoryId = categories[2].Id
                },
                new Product {
                    Name = "Model Y",
                    Description = "Compact SUV, AWD, 7 seats optional.",
                    Price = 51999,
                    Stock = 10,
                    IsApproved = true,
                    Image = "NewModelY_73.jpg",
                    IsHome = true,
                    CategoryId = categories[2].Id
                },
                
                new Product {
                    Name = "Roadster",
                    Description = "Supercar. 0-100 km/h under 2 seconds. Top speed 400+ km/h.",
                    Image = "0x0-Roadster_09.jpg",
                    Price = 199999,
                    Stock = 2,
                    IsApproved = true,
                    IsHome = false,
                    CategoryId = categories[5].Id
                }
            };

            foreach (var product in products)
                context.Products.Add(product);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
