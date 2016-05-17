using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using GetCats.Models.ApiModels;

namespace GetCats.Services
{
    public class OrderEmailService
    {
        private readonly SmtpClient _client;

        public OrderEmailService()
        {
            _client = new SmtpClient("smtp.gmail.com", 25)
            {
                UseDefaultCredentials = true,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new System.Net.NetworkCredential("getcats2016@gmail.com", "Sommar#2016")
            };
        }

        public void SendOrderConfirmation(string targetEmail, string orderNr, int total, List<PurchasedItemApiModel> items)
        {
            var itemHtml = "<table><tr><th>Name</th><th>Resolution</th><th>Url</th><th>Price</th></tr></thead><tbody>";
            foreach (var item in items)
            {
                itemHtml += $"<tr><td>{item.Name}</td><td>{item.Resolution.ToString()}</td><td>{item.Url}</td><td>{item.Price}</td></tr>";
            }
            itemHtml += "</tbody></table>";

            var body = "Thanks for your order!<br /><br />" +
                       $"<p><h2>Details</h2>Order number: {orderNr}<br />Total: {total}</p>" +
                       $"<p>The following items where ordered:<br />" +
                       itemHtml +
                       "</p>" +
                       "Thank you for your order!<br />//GetCats underpayed employees";
            var message = new MailMessage
            {
                From = new MailAddress("getcats2016@gmail.com", "GetCats - Your nr1 cat stock image shop!"),
                To = { new MailAddress(targetEmail) },
                Subject = $"Order {orderNr} placed!",
                Body = body,
                IsBodyHtml = true
            };
            _client.Send(message);
        }
    }
}