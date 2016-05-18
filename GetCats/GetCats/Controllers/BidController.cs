using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using GetCats.Models;
using GetCats.Models.Entities;
using GetCats.Models.ViewModels;
using GetCats.Services;
using Microsoft.AspNet.Identity;

namespace GetCats.Controllers
{
    public class BidController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly BidService _bidService;

        public BidController()
        {
            _bidService = new BidService();
        }

        protected override void Dispose(bool disposing)
        {
            if (db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ListBids()
        {
            
            var model = db.Bids.Where(x => x.Status.ToString().Equals(Bid.BidStatus.Initial.ToString())).Select(r => new ListBidViewModel
            {
               BidId = r.Id,
               Status = r.Status.ToString(),
               Bid = r.Price,       
               Email = r.Bidder.Email,
               ImageName = r.ImageOption.ParentImage.Name,
               Resolution = r.ImageOption.Resolution.ToString(),
               Price = r.ImageOption.Price
            }).ToList();

            foreach (var i in model)
            {
                Debug.WriteLine("Found bid.... " + i.Price);
            }
            return View(model);

        }

        [Authorize(Roles = "Admin")]
        public ActionResult ConfirmBid(string bidId, int status)
        {
            _bidService.AnswerBid(bidId, status);

            Debug.WriteLine("Sparade ändringar i databasen.");
            return RedirectToAction("ListBids");
        }
    }
}