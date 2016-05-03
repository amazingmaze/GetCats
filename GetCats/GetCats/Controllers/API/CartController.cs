using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace GetCats.Controllers.API
{
    [Authorize]
    public class CartController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> GetCart() //Returns all images registered in the database as a json array object
        {
            return Json("");
        }

        [HttpPut]
        public async Task<IHttpActionResult> AddToCart(int id) //Adds the purchaseoption with the supplied id as a cart item
        {
            return Json("");
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteFromCart(int id) //Deletes the purchaseoption with the supplied id as a cart item
        {
            return Json("");
        }

        [HttpPost]
        public async Task<IHttpActionResult> PurchaseCart() //Some function to buy the selected cart?!?!?!
        {
            return Json("");
        }
    }
}
