using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using MvcECommerce.Entities;
using MvcECommerce.Identity;
using MvcECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcECommerce.Controllers
{
    
    public class AccountController : Controller
    {   private DataContext _context = new DataContext();   
        private UserManager<ApplicationUser> _userManager; 
        private RoleManager<ApplicationRole> _roleManager;
        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new DataContext());
            _userManager = new UserManager<ApplicationUser>(userStore);
            var roleStore = new RoleStore<ApplicationRole>(new DataContext());
            _roleManager = new RoleManager<ApplicationRole>(roleStore); 
        }
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                //kayıt
                ApplicationUser user = new ApplicationUser();
                user.Name = model.Name; 
                user.Surname = model.Surname;
                user.UserName = model.Username;
                user.Email = model.Email;
                IdentityResult result = _userManager.Create(user , model.Password);
                if(result.Succeeded)
                {
                    
                    if(_roleManager.RoleExists("user")){
                        _userManager.AddToRole(user.Id, "user");
                    }
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError", "kullanici olusturma hatasi");
                }

            }
            
            return View(model); 
        }
        
        public ActionResult Details(int id)
        {
            var entity = _context.Orders
                .Where(i => i.Id == id)
                .Select(i => new OrderDetailsModel
                {
                    Id = i.Id,
                    OrderNumber = i.OrderNumber,
                    FullName = i.FullName,
                    Username = i.Username,
                    Total = i.Total,
                    OrderDate = i.OrderDate,
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
        
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var orders = _context.Orders.Where(i => i.Username == username).Select(i=>new UserOrderModel()
            {
                Id = i.Id,
                OrderDate = i.OrderDate,
                OrderNumber = i.OrderNumber,
                OrderState = i.OrderState,
                Total = i.Total,
            }).OrderByDescending(i=> i.OrderDate).ToList();
            return View(orders);


        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model , string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.Find(model.Username, model.Password);
                if(user!= null)
                {
                    //var olan kullanıcıyı sisteme dahil etmek
                    //application cookie 
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identityClaims = _userManager.CreateIdentity(user, "ApplicationCookie");
                    new AuthenticationProperties().IsPersistent = model.RememberMe;
                    authManager.SignIn(new AuthenticationProperties { IsPersistent = model.RememberMe }, identityClaims);
                    if (!string.IsNullOrEmpty(ReturnUrl)) {
                            return Redirect(ReturnUrl);
                    }
                        
                    return RedirectToAction("Index", "Home"); 

                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "Username or password wrong");
                }

            }

            return View(model);
        }
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index", "Home"); 

        }
    }
}