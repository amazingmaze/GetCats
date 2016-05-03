using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetCats.Models;
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

        public async Task<List<Image>> GetImages()
        {
            using (var context = ApplicationDbContext.Create())
            {
                return await context.Images.ToListAsync();
            }
        }
    }
}
