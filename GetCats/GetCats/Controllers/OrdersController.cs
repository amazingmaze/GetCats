using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GetCats.Models;
using GetCats.Models.DTO;
using GetCats.Models.ViewModels;
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

        // List order history
        public ActionResult Index()
        {
            var model = new OrderListViewModel
            {
                Orders = _context.Orders.Where(o => o.User.Email.Equals(User.Identity.Name)).Select(x => new OrderListItem
                {
                    Id = x.Id,
                    Created = x.Created,
                    Currency = x.Currency,
                    Status = x.Status,
                    Total = x.Total
                }).OrderByDescending(d => d.Created).ToList()
            };
            return View(model);
        }

        //View order
        public async Task<ActionResult> View(Guid? id)
        {
            if (id != null)
            {
                var order = await _context.Orders.FindAsync(id);
                if (order != null)
                {
                    var model = new OrderViewModel
                    {
                        Status = order.Status,
                        Created = order.Created,
                        StatusChanged = order.StatusChanged,
                        Id = order.Id,
                        Tax = order.Tax,
                        Shipping = order.Shipping,
                        Total = order.Total,
                        SubTotal = order.SubTotal,
                        Currency = order.Currency,
                        Items = order.Items.Select(x => new CartItem
                        {
                            Name = x.ParentImage.Name,
                            Resolution = x.Resolution.ToString(),
                            Price = x.Price,
                            ImageId = x.ParentImage.Id,
                        }).ToList()
                    };
                    return View(model);
                }
            }
            throw new HttpException(404, "Order not found");
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