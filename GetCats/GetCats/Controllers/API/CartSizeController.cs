using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GetCats.Services;

namespace GetCats.Controllers.API
{
    [Authorize]
    public class CartSizeController : ApiController
    {
        private readonly CartService _cartService;

        public CartSizeController()
        {

            _cartService = new CartService(); //Replace with injection?
        }

        [HttpGet]
        public IHttpActionResult GetSize() //Returns all images registered in the database as a json array object
        {
            return Json(new { Size = _cartService.GetCartSize() });
        }
    }
}
