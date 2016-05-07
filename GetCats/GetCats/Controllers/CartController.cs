using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using GetCats.Models;
using GetCats.Models.DTO;
using GetCats.Models.Entities;
using GetCats.Services;
using PayPal.Api;

namespace GetCats.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly PayPalService _paypalService;
        private readonly CartService _cartService;

        public CartController()
        {
            _paypalService = new PayPalService();
            _cartService = new CartService();
        }

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        private string getReturnUrl(string orderId)
        {
            return this.Url.Action("PayPalResponse", "Cart", new { id = orderId }, this.Request.Url.Scheme);
        }

        public ActionResult PayWithPayPal()
        {
            var apiContext = _paypalService.GetApiContext();
            if (_cartService.GetCartSize() == 0) return RedirectToAction("Index"); //Dont process if cart is empty
            var order = new Models.Entities.Order();

            using (var context = ApplicationDbContext.Create())
            {
                var payment = _paypalService.CreatePayment(new PaypalPaymentParams
                {
                    Context = apiContext,
                    Currency = PaypalPaymentParams.PaymentCurrency.EUR,
                    Items = _cartService.GetCartItems(),
                    OrderId = order.Id,
                    RedirectUrl = getReturnUrl(order.Id.ToString()),
                    Shipping = 1,
                    TaxPercentage = 0.2m
                });

                order.PaymentId = payment.id;
                context.Orders.Add(order);
                try
                {
                    var redirectUrl = GetPayPalRedirectUrl(payment);
                    order.User = context.Users.Find(User.Identity.Name);
                    //context.SaveChanges();
                    _cartService.ClearCart();
                    return Redirect(redirectUrl);
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.Message);
                    throw ex;
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

        public ActionResult PayPalResponse(string PayerID)
        {
            throw new Exception("GOD PAYERID: " + PayerID);
            
        }
    }
}