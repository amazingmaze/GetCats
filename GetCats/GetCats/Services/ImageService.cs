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
        public async Task<Image> GetImage(Guid id)
        {
            using (var context = ApplicationDbContext.Create())
            {
                return await context.Images.FindAsync(id);
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
                                  Options = image.Options.Select(x => new PurchaseOptionApiModel { Id = x.Id.ToString() }).ToArray()
                              }).ToList();
            }
        }


        // todo: ÄNDRA (BARA FÖR TESTNING)
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
