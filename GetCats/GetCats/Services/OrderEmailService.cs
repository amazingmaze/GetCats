using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using GetCats.Models.ApiModels;
using GetCats.Models.Entities;

namespace GetCats.Services
{
    public class OrderEmailService
    {
        private readonly SmtpClient _client;
        private static string _path = "/Images/";

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
            var url = HttpContext.Current.Request.Url.Authority + _path;

            var itemHtml = "<table><tr><th>Name</th><th>Resolution</th><th>Url</th><th>Price</th></tr></thead><tbody>";
            foreach (var item in items)
            {
                var itemUrl = url + item.Id + item.FileName;
                if (item.Resolution.Equals(PurchaseOption.ImageResolution.Low))
                {
                    itemUrl += "?w=200";
                }
                else if (item.Resolution.Equals(PurchaseOption.ImageResolution.High))
                {
                    itemUrl += "?w=400";
                }
                itemHtml += $"<tr><td>{item.Name}</td><td>{item.Resolution.ToString()}</td><td>{itemUrl}</td><td>{item.Price}</td></tr>";
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