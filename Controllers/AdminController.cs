using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MvcECommerce.Entities;
using MvcECommerce.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

public class AdminController : Controller
{
    private DataContext db = new DataContext();
    private UserManager<ApplicationUser> _userManager;
    private RoleManager<ApplicationRole> _roleManager;

    public AdminController()
    {
        var userStore = new UserStore<ApplicationUser>(db);
        _userManager = new UserManager<ApplicationUser>(userStore);

        var roleStore = new RoleStore<ApplicationRole>(db);
        _roleManager = new RoleManager<ApplicationRole>(roleStore);
    }

    // Kullanıcıları Listele
    [Authorize(Roles = "admin,superadmin")]
    public ActionResult Index()
    {
        var users = _userManager.Users.ToList();
        var userRoles = new Dictionary<string , List<string>>();

        foreach (var user in users)
        {
            userRoles[user.Id] = _userManager.GetRoles(user.Id).ToList();
        }
        var adminCount = users.Count(u =>
        userRoles[u.Id].Contains("admin") || userRoles[u.Id].Contains("superadmin"));
        ViewBag.AdminCount = adminCount;
        ViewBag.UserRoles = userRoles;
        return View(users);
    }

    // Kullanıcıyı sil
    [Authorize(Roles = "admin,superadmin")]
    public ActionResult Delete(string id)
    {
        var user = _userManager.FindById(id);
        if (user == null)
            return HttpNotFound();
        return View(user);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin,superadmin")]
    public ActionResult DeleteConfirmed(string id)
    {
        var user = _userManager.FindById(id);
        if (user != null)
            _userManager.Delete(user);

        return RedirectToAction("Index");
    }

    // Kullanıcı yetkisi değiştir (Edit)
    [Authorize(Roles = "admin,superadmin")]
    public ActionResult Edit(string id)
    {
        var user = _userManager.FindById(id);
        if (user == null)
            return HttpNotFound();

        var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();
        var currentRoles = _userManager.GetRoles(user.Id);

        ViewBag.Roles = new SelectList(allRoles, currentRoles.FirstOrDefault());
        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "admin,superadmin")]
    public ActionResult Edit(string id, string Role)
    {
        var user = _userManager.FindById(id);
        if (user == null)
            return HttpNotFound();

        var userRoles = _userManager.GetRoles(user.Id);
        foreach (var r in userRoles)
            _userManager.RemoveFromRole(user.Id, r);

        _userManager.AddToRole(user.Id, Role);

        return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            db.Dispose();
            _userManager.Dispose();
            _roleManager.Dispose();
        }
        base.Dispose(disposing);
    }
}