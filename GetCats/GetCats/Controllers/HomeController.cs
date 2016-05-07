using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using PayPal.Api;

namespace GetCats.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult PaypalTest()
        {
            ViewBag.Message = "Paypal test page.";

            // Authenticate with PayPal
            var config = ConfigManager.Instance.GetProperties();
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken);

            // Make an API call
            var payment = Payment.Create(apiContext, new Payment
            {
                intent = "sale",
                payer = new Payer
                {
                    payment_method = "paypal"
                },
                transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        description = "Transaction description.",
                        invoice_number = "001",
                        amount = new Amount
                        {
                            currency = "SEK",
                            total = "100.00",
                            details = new Details
                            {
                                tax = "15",
                                shipping = "10",
                                subtotal = "75"
                            }
                        },
                        item_list = new ItemList
                        {
                            items = new List<Item>
                            {
                                new Item
                                {
                                    name = "Sunglasses",
                                    currency = "SEK",
                                    price = "15",
                                    quantity = "5",
                                    sku = "sku"
                                }
                            }
                        }
                    }
                },
                redirect_urls = new RedirectUrls
                {
                    return_url = "http://mysite.com/return",
                    cancel_url = "http://mysite.com/cancel"
                }
            });
            payment.Execute(apiContext, new PaymentExecution());
            return View();
        }
    }
}