using MvcECommerce.Entities;
using MvcECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcECommerce.Controllers
{
    [Authorize(Roles = "admin , superadmin")]
    public class OrderController : Controller
    {   
        DataContext db = new DataContext();
        
        public ActionResult Index()
        {    
            var orders = db.Orders.Select(o => new AdminOrderModel
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                Total = o.Total,
                OrderDate = o.OrderDate,
                OrderState = o.OrderState,
                Count = o.OrderLines.Count
            }).OrderByDescending(i=>i.OrderDate).ToList();
            return View(orders);
        }

        [HttpPost]
        public ActionResult UpdateOrderState(int OrderId, EnumOrderState OrderState)
        {
            var order = db.Orders.FirstOrDefault(i => i.Id == OrderId);
            if (order != null)
            {
                order.OrderState = OrderState;
                db.SaveChanges();
                TempData["message"] = "Order status updated successfully.";
            }
            else
            {
                TempData["error"] = "Order not found.";
            }
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var entity = db.Orders
    .Where(i => i.Id == id)
    .Select(i => new OrderDetailsModel
    {   
        Id = i.Id,
        OrderNumber = i.OrderNumber,
        FullName = i.FullName,
        Username = i.Username,
        Total = i.Total,
        OrderDate = i.OrderDate,
        OrderId = i.Id,
        OrderState = i.OrderState,
        AddressName = i.AddressName,
        City = i.City,
        District = i.District,
        Neighborhood = i.Neighborhood,
        ZipCode = i.ZipCode,
        OrderLines = i.OrderLines.Select(a => new OrderLineModel
        {
            ProductId = a.ProductId,
            ProductName = a.Product.Name,
            Image = a.Product.Image,
            Quantity = a.Quantity,
            Price = a.Price
        }).ToList()
    }
    ).FirstOrDefault();

            if (entity == null)
                return HttpNotFound();

            return View(entity);
        }
    }
}