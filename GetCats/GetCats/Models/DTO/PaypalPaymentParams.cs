using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using PayPal.Api;

namespace GetCats.Models.DTO
{
    public class PaypalPaymentParams
    {
        public enum PaymentCurrency
        {
            SEK, USD, NOK, DKK, EUR
        }

        public List<CartItem> Items { get; set; }
        public APIContext Context { get; set; }
        public string RedirectUrl { get; set; }
        public Guid OrderId { get; set; }
        public PaymentCurrency Currency { get; set; }
        public decimal TaxPercentage { get; set; }
        public int Shipping { get; set; }

        public int Tax => Convert.ToInt32(Math.Ceiling(SubTotal * TaxPercentage));

        public int SubTotal => Items.Sum(item => item.Price);

        public int Total => Tax + SubTotal + Shipping;
    }
}
