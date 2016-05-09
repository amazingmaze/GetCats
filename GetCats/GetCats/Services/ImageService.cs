using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetCats.Models;
using GetCats.Models.ApiModels;
using GetCats.Models.Entities;

namespace GetCats.Services
{
    public class ImageService
    {
        public ImageApiModel GetImage(Guid id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                var image = context.Images.Find(id);

                return new ImageApiModel
                {
                    Id = image.Id.ToString(),
                    Name = image.Name,
                    FileName = image.FileName,
                    Options = image.Options.Select(x => new PurchaseOptionApiModel { Id = x.Id.ToString() }).ToArray()
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
