using System;
using System.Threading.Tasks;
using System.Web.Http;
using GetCats.Services;

namespace GetCats.Controllers.API
{
    [Authorize]
    public class ImagesController : ApiController
    {
        private readonly ImageService _imageService;

        public ImagesController()
        {
            _imageService = new ImageService(); //Replace with injection?
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetImages() //Returns all images registered in the database as a json array object
        {
            return Json(await _imageService.GetImages());
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetImage(Guid id) //Returns an image with the matchion guid as a JSON array object (or returns null)
        {
            return Json(await _imageService.GetImage(id));
        }
    }
}
