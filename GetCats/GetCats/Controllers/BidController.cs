using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GetCats.Models;

namespace GetCats.Controllers
{
    public class BidController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected override void Dispose(bool disposing)
        {
            if (db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ListBids()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ApproveBid()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DisapproveBid()
        {
            return View();
        }
    }
}