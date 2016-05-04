using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using GetCats.Models.DTO;
using GetCats.Models.Entities;
using GetCats.Services;

namespace GetCats.Controllers.API
{
    [Authorize]
    public class CartController : ApiController
    {
        private readonly CartService _cartService;

        public CartController()
        {
            
            _cartService = new CartService(); //Replace with injection?
        }

        [HttpGet]
        public IHttpActionResult GetCart() //Returns all images registered in the database as a json array object
        {
            var items = _cartService.GetCartItems();
            var cartResult = new CartResult
            {
                Items = items,
                ItemsInCart = items.Count,
                TotalPrice = items.Sum(item => item.Price)
            };
            return Json(cartResult);
        }

        [HttpPut]
        public void AddToCart(Guid id) //Adds the purchaseoption with the supplied id as a cart item
        {
            _cartService.AddItemToCart(id);
        }

        [HttpDelete]
        public void DeleteFromCart(Guid id) //Deletes the purchaseoption with the supplied id as a cart item
        {
            _cartService.RemoveItemFromCart(id);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Checkout() //Some function to buy the selected cart?!?!?!
        {
            return Json("");
        }

        
    }
}
