using MvcECommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MvcECommerce.Models; // Include için!

namespace MvcECommerce.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();

        // Ana sayfa ürünleri
        public ActionResult Index()
        {
            var products = db.Products.Where(i => i.IsApproved && i.IsHome)
                .Select(i => new ProductModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description.Length > 50 ? i.Description.Substring(0,47)+ "...":i.Description,
                    Image = i.Image, 
                    Price = i.Price,
                    Stock = i.Stock,                                             
                    IsApproved = i.IsApproved,
                    IsHome = i.IsHome,
                    CategoryId = i.CategoryId
                }).ToList();
            return View(products); 
        }

        // Ürün detayı (kategoriyle birlikte)
        public ActionResult Details(int id)
        {
            var product = db.Products
                .Include(p => p.Category)
                .FirstOrDefault(i => i.Id == id);

            return View(product);
        }

        // Ürün listesi
        public ActionResult List(int? id)
        {
            var products = db.Products.Where(i => i.IsApproved)
                .Select(i => new ProductModel
                {
                    Id = i.Id,
                    Name = i.Name.Length >50 ? i.Name.Substring(0,47) + "..." : i.Name,
                    Description = i.Description.Length > 50 ? i.Description.Substring(0, 47) + "..." : i.Description,
                    Image = i.Image == null ? "bg.jpg" : i.Image,
                    Price = i.Price,
                    Stock = i.Stock,
                    IsApproved = i.IsApproved,
                    IsHome = i.IsHome,
                    CategoryId = i.CategoryId
                }).AsQueryable();
            if(id != null)
            {
                    products = products.Where(i => i.CategoryId == id); 
            }
            return View(products.ToList());
        }
        public PartialViewResult GetCategories()
        { 
            return PartialView(db.Categories.ToList());
        }
    }
}
