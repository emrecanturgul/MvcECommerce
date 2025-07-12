using MvcECommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MvcECommerce.Models; 

namespace MvcECommerce.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();

        
        public ActionResult Index()
        {
           
                var products = db.Products
                .Where(i => i.IsApproved && i.IsHome)
                .ToList()
                .Select(i => new ProductModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                    Image = i.Image,
                    Price = i.Price,
                    Stock = i.Stock,
                    IsApproved = i.IsApproved,
                    IsHome = i.IsHome,
                    CategoryId = i.CategoryId
                }).ToList();
            return View(products);
        }

        
        public ActionResult Details(int id)
        {
            var product = db.Products
                .Include(p => p.Category)
                .FirstOrDefault(i => i.Id == id);

            return View(product);
        }

    
        public ActionResult List(int? id)
        {
            var productEntities = db.Products.Where(i => i.IsApproved).ToList();

            if (id != null)
            {
                productEntities = productEntities.Where(i => i.CategoryId == id).ToList();
            }

            var products = productEntities.Select(i => new ProductModel
            {
                Id = i.Id,
                Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                Image = string.IsNullOrEmpty(i.Image) ? "bg.jpg" : i.Image,
                Price = i.Price,
                Stock = i.Stock,
                IsApproved = i.IsApproved,
                IsHome = i.IsHome,
                CategoryId = i.CategoryId
            }).ToList();

            return View(products);
        }

       
        public ActionResult Search(string q)
        {
            var searchEntities = db.Products
                .Where(x => x.IsApproved &&
                    (x.Name.Contains(q) || x.Description.Contains(q)))
                .ToList();

            var searchResults = searchEntities.Select(i => new ProductModel
            {
                Id = i.Id,
                Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                Image = string.IsNullOrEmpty(i.Image) ? "bg.jpg" : i.Image,
                Price = i.Price,
                Stock = i.Stock,
                IsApproved = i.IsApproved,
                IsHome = i.IsHome,
                CategoryId = i.CategoryId
            }).ToList();

            return View("Search", searchResults); 
        }

        public PartialViewResult GetCategories()
        {
            return PartialView(db.Categories.ToList());
        }
    }
}
