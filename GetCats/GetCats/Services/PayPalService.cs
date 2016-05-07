using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using GetCats.Models.DTO;
using PayPal.Api;

namespace GetCats.Services
{
    public class PayPalService
    {
        private readonly string _oauthToken;

        public PayPalService()
        {
            var config = ConfigManager.Instance.GetProperties();
            _oauthToken = new OAuthTokenCredential(config).GetAccessToken();

        }

        public APIContext GetApiContext()
        {
            return new APIContext(_oauthToken);
        }

        public Payment ExecutePayment(APIContext context, string payerId, string paymentId)
        {
            var payment = new Payment() { id = paymentId };
            return payment.Execute(context, new PaymentExecution() { payer_id = payerId });
        }

        public Payment CreatePayment(PaypalPaymentParams paymentParams)
        {
            var items = CartItemsToPayPalItems(paymentParams.Items, paymentParams.Currency.ToString());
            var redirectUrls = new RedirectUrls() { cancel_url = paymentParams.RedirectUrl, return_url = paymentParams.RedirectUrl };

            var amount = new Amount()
            {
                currency = paymentParams.Currency.ToString(),
                total = paymentParams.Total.ToString(),
                details = new Details()
                {
                    tax = paymentParams.Tax.ToString(),
                    shipping = paymentParams.Shipping.ToString(),
                    subtotal = paymentParams.SubTotal.ToString()
                }
            };

            var transactions = CreateTransaction("GetCatz Cart Checkout", paymentParams.OrderId.ToString(), amount, items);

            foreach (var item in items.items)
            {
                Debug.WriteLine("[" + item.price + ", " + item.currency + ", " + item.name + ", " + item.sku + ", " + item.tax + ", " + item.quantity);
            }

            return CreatePaymentInstance(transactions, redirectUrls, paymentParams.Context);
        }

        private ItemList CartItemsToPayPalItems(List<CartItem> items, string currency)
        {
            var itemList = items.Select(x => new Item
            {
                name = x.Name,
                currency = currency,
                price = x.Price.ToString(),
                quantity = "1",
                sku = x.PurchaseOptionId.ToString()
            }).ToList();
            return new ItemList { items = itemList };
        }

        private List<Transaction> CreateTransaction(string description, string orderId, Amount amount, ItemList items)
        {
            return new List<Transaction>
            {
                new Transaction
                {
                    description = description,
                    invoice_number = orderId,
                    amount = amount,
                    item_list = items
                }
            };
        }

        private Payment CreatePaymentInstance(List<Transaction> transactions, RedirectUrls urls, APIContext context)
        {
            var payment = new Payment
            {
                intent = "sale",
                payer = new Payer() { payment_method = "paypal" /*, payer_info = GetPayerInfo() */ },
                transactions = transactions,
                redirect_urls = urls
            };
            return payment.Create(context);
        }

        //DETTA VERKAR ÖHT EJ FUNGERA KORREKT I PAYPAL SDK
        private PayerInfo GetPayerInfo()
        {
            return new PayerInfo
            {
                first_name = "Adam",
                last_name = "Bertil",
                billing_address = new Address { city = "ktown", country_code = "SE", line1 = "Bokgatan 1", postal_code = "12343", state = "Kristianstad", phone = "123456"},
                shipping_address = new ShippingAddress { city = "ktown", country_code = "SE", line1 = "Bokgatan 1", postal_code = "12343", state = "Kristianstad", phone = "123456" }
            };
        }
    }
}