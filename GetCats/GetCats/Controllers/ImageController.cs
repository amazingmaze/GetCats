﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using GetCats.Models;
using GetCats.Models.Entities;
using GetCats.Models.ViewModels;
using GetCats.Services;

namespace GetCats.Controllers
{
    public class ImageController : Controller
    {
        private readonly ImageService _imageService;
        private readonly BidService _bidService; 
        private ApplicationDbContext db = new ApplicationDbContext();

        protected override void Dispose(bool disposing)
        {
            if (db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ImageController()
        {
            _imageService = new ImageService(); //Replace with injection?
            _bidService = new BidService();
        }

        [Authorize]
        public ActionResult List(string search)
        {
            Debug.WriteLine("LIST METHOD USED ");
            if (String.IsNullOrEmpty(search))
        {
                Debug.WriteLine("search contains nothing ");
            return View();
        }
            var model =
                db.Images.Where(x => search == null || x.Name.StartsWith(search))
                    .Take(10)
                    .Select(u => new ImageViewModel
                    {
                        Id = u.Id,
                        Name = u.Name,
                    }).ToList();
            Debug.WriteLine("search contains SOMETHING ");
            return View(model);
        }

        [Authorize]
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View("List");
            }
            // Get image from web service
            var img = _imageService.GetImage(Guid.Parse(id));

            return View(img);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Bid(string optionId, string bid, string id)
        {
            // Add bid to db
            _bidService.CreateBid(optionId, bid);
            return RedirectToAction("Details", "Image", new {id});
        }

        public ActionResult RemoveBid(string id, string bidId)
        {
            // Remove bid from db
            Debug.WriteLine("Removing bid... : " + bidId);

            _bidService.RemoveBid(bidId);

            return RedirectToAction("Details", "Image", new { id });
        }


        //[Authorize(Roles = "Admin")] todo: CHANGE BACK
        public ActionResult UploadImage()
        {
            return View(new ImageAddViewModel());
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost] // todo: KOLLA ÖVER DENNA
        public ActionResult UploadImage(ImageAddViewModel model)
        {

            if (ModelState.IsValid)
            {
                model.FileName = model.File.FileName;

                var img = new Image
                {
                    FileName = model.FileName,
                    Name = model.Name
                };

                // Get id back
                var imgId = _imageService.InsertImage(img, new PurchaseOption[]
                {
                    model.LowRes,
                    model.HighRes,
                    model.MaxRes
                }
                    );


                // Save image to disk ( /images/{id + fileName}
                var path = System.IO.Path.Combine(Server.MapPath("~/Images/"), imgId + model.File.FileName);
                model.File.SaveAs(path);

                return RedirectToAction("List", "Image");
            }


            return View();
        }
    }
}