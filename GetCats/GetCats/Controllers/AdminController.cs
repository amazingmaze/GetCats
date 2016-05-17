using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
            var userList = new List<AdminViewModel>();
            var users = db.Users.ToList();
            foreach (var user in users)
            {
                var roles = UserManager.GetRoles(user.Id);
                var roleName = "User";
                if (roles.Contains("Admin"))
                {
                    roleName = "Admin";
                }

                userList.Add(new AdminViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Street = user.Street,
                    Country = user.Country.Name,
                    Region = user.Region,
                    PostalCode = user.PostalCode,
                    Status = user.Status.ToString(),
                    Permission = roleName
                });
            }

            return View(userList);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            AdminViewModel model = null;
            var permission = "";
            var status = "";
            var roles = UserManager.GetRoles(id);
            var isLockedOut = UserManager.IsLockedOut(id); 
            Debug.WriteLine(UserManager.GetLockoutEnabled(id));
            Debug.WriteLine("Islockedout... " + UserManager.IsLockedOut(id));

            if (roles.Contains("Admin"))
            {
                permission = "1";
            }
            else
            {
                permission = "0";
            }
            if(isLockedOut.Equals(true) )
            {
                status = "0";
            }
            else
            {
                status = "1";
            }

            model = db.Users.Where(u => u.Id.Equals(id)).Select(o => new AdminViewModel
            {
                Street = o.Street,
                Region = o.Region,
                Country = o.Country.Id.ToString(),
                PostalCode = o.PostalCode,
                Email = o.Email,
                Status = status,
                Id = o.Id,
                Countries = db.Countries.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList(),
                Permission = permission,
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

