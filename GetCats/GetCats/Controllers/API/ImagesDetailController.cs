using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using GetCats.Models.ApiModels;
using GetCats.Services;

namespace GetCats.Controllers.API
{
    [Authorize]
    public class ImagesDetailController : ApiController
    {
        private readonly ImageService _imageService;

        public ImagesDetailController()
        {
            _imageService = new ImageService(); //Replace with injection?
        }

        [HttpGet]
        public IHttpActionResult GetImage(Guid id) //Returns an image with the matchion guid as a JSON array object (or returns null)
        {
            return Json(_imageService.GetImage(id));
        }
    }
}
