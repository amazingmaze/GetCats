using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GetCats.Controllers.API;
using GetCats.Models;
using GetCats.Models.Entities;
using GetCats.Models.ViewModels;
using GetCats.Services;

namespace GetCats.Controllers
{
    public class ImageController : Controller
    {
        private readonly ImageService _imageService;

        public ImageController()
        {
            _imageService = new ImageService(); //Replace with injection?
        }

        [Authorize]
        public ActionResult List()
        {
            return View();
        }

        [Authorize]
        public ActionResult Details(string id)
        {
            // Get image from web service
            var img = _imageService.GetImage(Guid.Parse(id));
            // Pass into view
            return View(img);
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

                // Insert model (or entity) into Db          
                var imgService = new ImageService();

                var img = new Image
                {
                    FileName = model.FileName,
                    Name = model.Name
                };

                var opt1 = new PurchaseOption {Price = model.Options.Price, Resolution = model.Options.Resolution};

                // Get id back?
                var imgId = imgService.InsertImage(img, new PurchaseOption[]
                {
                    opt1
                }
                    );

                // Save image to disk ( /images/{id + fileName?}
                var path = System.IO.Path.Combine(Server.MapPath("~/Images/"), imgId + model.File.FileName);
                model.File.SaveAs(path);

                return RedirectToAction("List", "Image");
            }


            return View();
        }
    }
}