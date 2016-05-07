using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GetCats.Models;
using GetCats.Services;

namespace GetCats.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrdersController()
        {
            _context = ApplicationDbContext.Create();
        }

        // List order hisotory
        public ActionResult Index()
        {
            return View();
        }

        //View order
        public async Task<ActionResult> View(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}