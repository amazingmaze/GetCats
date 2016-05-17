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
    public class ImageService
    {
        public ImageApiModel GetImage(Guid id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var image = context.Images.Find(id);
                var user = context.Users.Find(HttpContext.Current.User.Identity.GetUserId());

                // Test
                var test = context.Bids.Where(x => x.Bidder.Email.Equals(user.Email)).ToList();
                foreach (var t in test)
                {
                    Debug.WriteLine("Bid Id: " + t.Id + " : Bid Price: " + t.Price);
                }

                return new ImageApiModel
                {
                    Id = image.Id.ToString(),
                    Name = image.Name,
                    FileName = image.FileName,
                    Options = image.Options.Select(x => new PurchaseOptionApiModel { Id = x.Id.ToString(), Resolution = x.Resolution, Price = x.Price, Bid = x.Bids.Where(u => u.Bidder.Email.Equals(user.Email)).Select(b => new OptionBidViewModel { Bid = b.Price, Status = b.Status }).FirstOrDefault() }).ToArray()
                };
            }
        }


        public List<ImageApiModel> GetImages()
        {
            using (var context = ApplicationDbContext.Create())
            {
               
                return (from image in context.Images.ToList()
                              select new ImageApiModel
                              {
                                  Id = image.Id.ToString(),
                                  Name = image.Name,
                                  FileName = image.FileName,
                                  Options = image.Options.Select(x => new PurchaseOptionApiModel { Id = x.Id.ToString() }).ToArray()
                              }).ToList();
            }
        }

        public Guid InsertImage(Image img, PurchaseOption[] options)
        {

            using (var context = ApplicationDbContext.Create())
            {
                foreach (var option in options)
                {
                    img.Options.Add(option);
                }
                context.Images.Add(img);
                context.SaveChanges();
                return img.Id;
            }
        }
    }
}
