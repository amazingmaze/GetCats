using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GetCats.Models;
using GetCats.Models.ApiModels;
using GetCats.Models.Entities;
using GetCats.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace GetCats.Services
{
    public class BidService
    {
        public Bid GetBid(string optId)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var user = context.Users.Find(HttpContext.Current.User.Identity.GetUserId());
                var option = context.PurchaseOptions.Find(optId);
                return (Bid) from bid in context.Bids where bid.Bidder.Equals(user) && bid.ImageOption.Equals(option) select bid;
            }  
        }

        public void CreateBid(string optionId, string bid)
        {
            using (var context = ApplicationDbContext.Create())
            {                     

                context.Bids.Add(
                    new Bid
                    {
                        Bidder = context.Users.Find(HttpContext.Current.User.Identity.GetUserId()),
                        ImageOption = context.PurchaseOptions.Find(Guid.Parse(optionId)),
                        Price = int.Parse(bid)
                    });
                context.SaveChanges();
            }
        }

        public void AnswerBid(string bidId, int status) {

            using (var context = ApplicationDbContext.Create())
            {

                var id = Guid.Parse(bidId);
                var bid = context.Bids.FirstOrDefault(x => x.Id.Equals(id));

                if (bid != null)
                {
                    Debug.WriteLine("Kom hit, (hittade bid i dbn. bIDoPTIONrES:" + bid.ImageOption.Resolution + " Bidder: " + bid.Bidder.Email);
                    bid.Status = (Bid.BidStatus)status;
                    context.Entry(bid).State = EntityState.Modified;
                }

                context.SaveChanges();
            }

        }

        public void RemoveBid(string bidId)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var bidGuid = Guid.Parse(bidId);
                var bid = context.Bids.FirstOrDefault(x => x.Id.Equals(bidGuid)); //returns a single item.

                if (bid != null)
                {
                    context.Bids.Remove(bid);
                    context.SaveChanges();
                }
            }
        }
    }
}
