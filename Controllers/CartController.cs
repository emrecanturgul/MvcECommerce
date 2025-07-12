using Microsoft.EntityFrameworkCore.Query.Internal;
using MvcECommerce.Entities;
using MvcECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcECommerce.Controllers
{
    public class CartController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View(GetCart());
        }
        public ActionResult AddToCart(int id, int quantity)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == id && i.IsApproved);
            if (product != null)
            {
                GetCart().AddProduct(product, quantity);
            }
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromCart(int id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == id);
            if (product != null)
            {
                GetCart().DecreaseProductQuantity(id);

            }
            return RedirectToAction("Index");
        }
        public Cart GetCart()
        {
            Cart cart = (Cart)Session["cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["cart"] = cart;
            }
            return cart;
        }
        [HttpGet]
        public ActionResult ChangeStatus(int id)
        {
            var order = db.Orders.Find(id);
            if (order == null)
                return HttpNotFound();
            return View(order);
        }

        [HttpPost]
        public ActionResult ChangeStatus(int id, EnumOrderState OrderState)
        {
            var order = db.Orders.Find(id);
            if (order == null)
                return HttpNotFound();

            order.OrderState = OrderState;
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public PartialViewResult Summary()
        {
            return PartialView(GetCart());


        }
        public ActionResult Checkout() {
            return View(new ShippingDetails());
        }
        [HttpPost]
        public ActionResult Checkout(ShippingDetails entity) {
            var cart = GetCart();
            if (cart.CartLines.Count == 0)
            {
                ModelState.AddModelError("NoItemsError", "no items in your cart");
            }
            if (ModelState.IsValid) {
                SaveOrder(cart, entity);
                Session["cart"] = null; // Clear the cart from session
              
                return View("Completed");
            }
            else
            {
                return View(entity);
            }

        }
        public ActionResult Completed()
        {
            return View();
        }
        
        Random rnd = new Random();
        private void SaveOrder(Cart cart, ShippingDetails entity)
        {
            
            var order = new Order();
            order.OrderNumber = rnd.Next(1000, 9999).ToString();
            order.Total = cart.Total();
            order.OrderDate = DateTime.Now;
            order.OrderState = EnumOrderState.Waiting;
            order.FullName = entity.FullName;
            order.Username = User.Identity.Name;
            order.AddressName = entity.AddressName;
            order.Address = entity.Address;
            order.City = entity.City;
            order.District = entity.District;
            order.Neighborhood = entity.Neighborhood;
            order.ZipCode = entity.ZipCode;
            order.OrderLines = new List<OrderLine>();
            foreach (var pr in cart.CartLines)
            {
                var orderLine = new OrderLine();
                orderLine.Quantity = pr.Quantity;
                orderLine.Price = pr.Product.Price * pr.Quantity;
                orderLine.ProductId = pr.Product.Id;
                order.OrderLines.Add(orderLine);

            }
            db.Orders.Add(order);
            db.SaveChanges();


        }

    }


    }
