using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GetCats.Models;
using GetCats.Models.ViewModels;

namespace GetCats.Controllers
{
    public class ImageController : Controller
    {
        [Authorize]
        public ActionResult List()
        {
            return View(GetPlaceholders());
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            return View(GetPlaceHolder(id));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UploadImage()
        {
            return View(new ImageAddViewModel());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UploadImage(ImageAddViewModel model)
        {

            if (ModelState.IsValid)
            {
                model.FileName = model.File.FileName;
                // Insert model (or entity) into Db

                // Get id back?

                // Save image to disk ( /images/{id + fileName?}
                var path = System.IO.Path.Combine(Server.MapPath("~/Images/"), "1" + model.File.FileName);
                model.File.SaveAs(path);

                return RedirectToAction("List", "Image");
            }


            return View();
        }



        public List<ImageViewModel> GetPlaceholders()
        {
            var images = new List<ImageViewModel>
            {
                new ImageViewModel
                {
                    Id = 1,
                    BuyOutPrice = 10.0,
                    Src =
                       "http://img.photobucket.com/albums/v162/ladynaoko/00005bf2.jpg",
                    Title = "Orange Cat",
                    CurrentBid = 15.0
                },
                new ImageViewModel
                {
                    Id = 2,
                    BuyOutPrice = 15.0,
                    Src =
                        "https://i.ytimg.com/vi/Bkco3bE2tg8/hqdefault.jpg",
                    Title = "Black Cat",
                    CurrentBid = 15.0
                },
                new ImageViewModel
                {
                    Id = 2,
                    BuyOutPrice = 15.0,
                    Src =
                        "http://i.kinja-img.com/gawker-media/image/upload/s--xusFpfJc--/1993ppenk3nvfjpg.jpg",
                    Title = "Black Cat",
                    CurrentBid = 15.0
                },
                new ImageViewModel
                {
                    Id = 2,
                    BuyOutPrice = 15.0,
                    Src =
                        "https://s-media-cache-ak0.pinimg.com/236x/f1/ef/2d/f1ef2d479b8d03bbe65ae59e20be6521.jpg",
                    Title = "Black Cat",
                    CurrentBid = 15.0
                },
                new ImageViewModel
                {
                    Id = 2,
                    BuyOutPrice = 15.0,
                    Src =
                        "http://new1.fjcdn.com/pictures/Lolcats_63d325_895786.jpg",
                    Title = "Black Cat",
                    CurrentBid = 15.0
                },
                new ImageViewModel
                {
                    Id = 2,
                    BuyOutPrice = 15.0,
                    Src =
                        "http://weknowmemes.com/wp-content/uploads/2012/04/its-cool-man-shes-18.jpg",
                    Title = "Black Cat",
                    CurrentBid = 15.0
                },
                new ImageViewModel
                {
                    Id = 2,
                    BuyOutPrice = 15.0,
                    Src =
                        "http://bonuscats.com/images/lolcats-2_13_2152.jpg",
                    Title = "Black Cat",
                    CurrentBid = 15.0
                }
            };

            return images;
        }

        public ImageViewModel GetPlaceHolder(int id)
        {
            return (from img in GetPlaceholders() where img.Id == id select img).First();
        }
    }
}