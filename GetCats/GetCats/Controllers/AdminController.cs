using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GetCats.Models;
using GetCats.Models.Entities;
using GetCats.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;


namespace GetCats.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        public AdminController() { }
        public AdminController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }
        
    
        protected override void Dispose(bool disposing)
        {
            if (db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var permission = "";
            if (User.IsInRole("Admin"))
            {
                permission = Models.Entities.User.PermissionStatus.Admin.ToString();
            }
            else
            {
                permission = Models.Entities.User.PermissionStatus.User.ToString();
            }
            var model = db.Users.Select(user => new AdminViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Street = user.Street,
                Country = user.Country.Name,
                Region = user.Region,
                PostalCode = user.PostalCode,
                Status = user.Status.ToString(),
                Permission = permission
            }).ToList();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            AdminViewModel model = null;
            var permission = "";
            if (User.IsInRole("Admin"))
            {
                permission = "1";
            }
            else
            {
                permission = "0";
            }
            model = db.Users.Where(u => u.Id.Equals(id)).Select(o => new AdminViewModel
            {
                Street = o.Street,
                Region = o.Region,
                Country = o.Country.Id.ToString(),
                PostalCode = o.PostalCode,
                Email = o.Email,
                Status = o.Status.ToString(),
                Id = o.Id,
                Countries = db.Countries.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList(),
                Permission = permission
            }).First();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdminViewModel model)
        {       
            if (ModelState.IsValid)
            {
                if (model.Status == "0")
                {
                    UserManager.SetLockoutEndDate(model.Id, DateTime.Today.AddYears(10));
                }
                else 
                {
                    UserManager.SetLockoutEndDate(model.Id, DateTime.Today.AddDays(-1));
                }
                if(model.Permission == "0")
                {
                    UserManager.RemoveFromRole(model.Id, "Admin");                  
                } 
                else
                {
                    UserManager.AddToRole(model.Id, "Admin");
                }
                var cId = Guid.Parse(model.Country);
                using (var context = ApplicationDbContext.Create())
                {
                    var user = context.Users.First(u => u.Id.Equals(model.Id));
                    user.Street = model.Street;
                    user.Country = context.Countries.First(x => x.Id.Equals(cId));
                    user.Email = model.Email;
                    user.PostalCode = model.PostalCode;
                    user.Region = model.Region;         
                    if (model.Status.Equals("0"))
                    {
                        user.Status = Models.Entities.User.UserStatus.Inactive;
                    }
                    else
                    {
                        user.Status = Models.Entities.User.UserStatus.Active;
                    }
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            
            
            return View();

        }
    }

}

