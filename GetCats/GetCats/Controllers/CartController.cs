using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using GetCats.Models;
using GetCats.Models.ApiModels;
using GetCats.Models.DTO;
using GetCats.Models.Entities;
using GetCats.Services;
using PayPal.Api;
using Order = GetCats.Models.Entities.Order;

namespace GetCats.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly PayPalService _paypalService;
        private readonly CartService _cartService;
        private readonly ApplicationDbContext _context;
        private readonly OrderEmailService _emailService;

        public CartController()
        {
            _paypalService = new PayPalService();
            _cartService = new CartService();
            _context = ApplicationDbContext.Create();
            _emailService = new OrderEmailService();
        }

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        private string GetReturnUrl(string orderId)
        {
            return this.Url.Action("FinalizePay", "Cart", new { id = orderId }, this.Request.Url.Scheme);
        }

        //Finalize payment
        public ActionResult FinalizePay(Guid id, string PayerId, string paymentId, string token)
        {
            //var order = _context.Orders.FirstOrDefault(o => o.Id.Equals(id)); VARFÖR I HELVETE FUNGERAR EJ DETTA..
            var user = _context.Users.First(u => u.Email.Equals(User.Identity.Name));
            var order = user.Orders.FirstOrDefault(o => o.Id.Equals(id));
            if (order != null)
            {
                var payment = _paypalService.ExecutePayment(_paypalService.GetApiContext(), PayerId, paymentId);
                if (payment.state.ToLower().Equals("approved"))
                {
                    order.Status = Order.OrderStatus.Payed;
                    order.StatusChanged = DateTime.Now;
                    _context.SaveChanges();
                    _emailService.SendOrderConfirmation(User.Identity.Name, order.Id.ToString(), order.Total, GetPurchasedItems(order));
                    return RedirectToAction("View", new { controller = "Orders", id });
                }
            }

            throw new Exception("Payment failed");
        }

        private List<PurchasedItemApiModel> GetPurchasedItems(Order order)
        {
            var items = new List<PurchasedItemApiModel>();
            foreach (var option in order.Items)
            {
                var price = option.Price;
                var email = HttpContext.User.Identity.Name;
                var bid = _context.Bids.FirstOrDefault(x => x.ImageOption.Id.Equals(option.Id) && x.Bidder.Email.Equals(email));
                if (bid != null && bid.Status == Bid.BidStatus.Approved)
                {
                    price = bid.Price;
                }

                items.Add(new PurchasedItemApiModel
                {
                    Name = option.ParentImage.Name,
                    Price = price,
                    Resolution = option.Resolution,
                    Id = option.ParentImage.Id,
                    FileName = option.ParentImage.FileName
                });
            }
            return items;
        }

        public async Task<ActionResult> Pay()
        {
            var apiContext = _paypalService.GetApiContext();
            if (_cartService.GetCartSize() == 0) return RedirectToAction("Index"); //Dont process if cart is empty
            var order = new Models.Entities.Order();

            var paymentParams = new PaypalPaymentParams
            {
                Context = apiContext,
                Currency = PaypalPaymentParams.PaymentCurrency.EUR,
                Items = _cartService.GetCartItems(),
                OrderId = order.Id,
                RedirectUrl = GetReturnUrl(order.Id.ToString()),
                Shipping = 1,
                TaxPercentage = 0.2m
            };
            var payment = _paypalService.CreatePayment(paymentParams);

            var user = await _context.Users.Where(u => u.Email.Equals(User.Identity.Name)).FirstAsync();
            order.PaymentId = payment.id;
            order.Total = paymentParams.Total;
            order.SubTotal = paymentParams.SubTotal;
            order.Tax = paymentParams.Tax;
            order.Currency = paymentParams.Currency;
            order.Shipping = paymentParams.Shipping;
            InsertPurchaseOptions(order);
            user.Orders.Add(order);
            try
            {
                var redirectUrl = GetPayPalRedirectUrl(payment);
                _context.SaveChanges();
                _cartService.ClearCart();
                return Redirect(redirectUrl);
            }
            catch (Exception ex)
            {
                while (ModelState.GetEnumerator().MoveNext())
                {
                    Debug.WriteLine(ModelState.GetEnumerator().Current.Value.Errors.First());
                }
                Debug.Write(ex.Message);
                throw ex;
            }
        }

        private void InsertPurchaseOptions(Models.Entities.Order order)
        {
            foreach (var cartItem in _cartService.GetCartItems())
            {
                var option = _context.PurchaseOptions.Find(cartItem.PurchaseOptionId);
                if (option != null)
                {
                    order.Items.Add(option);
                }
            }
        }

        private static string GetPayPalRedirectUrl(PayPalRelationalObject payment)
        {
            var links = payment.links.GetEnumerator();
            while (links.MoveNext())
            {
                var lnk = links.Current;
                if (lnk != null && lnk.rel.ToLower().Trim().Equals("approval_url")) { return lnk.href; }
            }
            return null;
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