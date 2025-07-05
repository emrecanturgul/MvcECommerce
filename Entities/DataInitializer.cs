using System.Collections.Generic;
using System.Data.Entity;

namespace MvcECommerce.Entities
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
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
                    Price = 79999,
                    Stock = 5,
                    IsApproved = true,
                    IsHome = true,
                    CategoryId = categories[1].Id // Sedan
                },
                new Product {
                    Name = "Model 3",
                    Description = "Affordable, long-range sedan. 0-100 in 3.3s.",
                    Price = 39999,
                    Stock = 8,
                    IsApproved = true,
                    IsHome = true,
                    CategoryId = categories[1].Id // Sedan
                },
                new Product {
                    Name = "Model X",
                    Description = "SUV with falcon doors, 7 seats. 0-100 in 2.6s.",
                    Price = 89999,
                    Stock = 4,
                    IsApproved = true,
                    IsHome = true,
                    CategoryId = categories[2].Id // SUV
                },
                new Product {
                    Name = "Model Y",
                    Description = "Compact SUV, AWD, 7 seats optional.",
                    Price = 51999,
                    Stock = 10,
                    IsApproved = true,
                    IsHome = true,
                    CategoryId = categories[2].Id // SUV
                },
                new Product
                {Name = "Model Y",
                    Description = "Compact SUV, AWD, 7 seats optional.",
                    Price = 51999,
                    Stock = 10,
                    IsApproved = true,
                    IsHome = true,
                    CategoryId = categories[2].Id

                },
                new Product {
                    Name = "Egea",
                    Description = "FGayis.",
                    Price = 59999,
                    Stock = 6,
                    IsApproved = true,
                    IsHome = true,
                    CategoryId = categories[4].Id // Cyber
                },
                new Product {
                    Name = "Roadster",
                    Description = "Supercar. 0-100 km/h under 2 seconds. Top speed 400+ km/h.",
                    Price = 199999,
                    Stock = 2,
                    IsApproved = true,
                    IsHome = false,
                    CategoryId = categories[5].Id // Supercar
                }
            };

            foreach (var product in products)
                context.Products.Add(product);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
