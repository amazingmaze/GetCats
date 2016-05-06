using System;
using System.Collections.Generic;
using System.Web;
using GetCats.Models.DTO;
using GetCats.Models.Entities;
using System.Linq;
using GetCats.Models;

namespace GetCats.Services
{
    public class CartService
    {
        public bool AddItemToCart(Guid purchaseOptionId)
        {
            var result = false;
            var items = GetCartItems();
            if (items.Count(x => x.PurchaseOptionId.Equals(purchaseOptionId)) == 0) //Cannot add the same item twice (nonsensical for a digital image)
            {
                using (var context = ApplicationDbContext.Create())
                {
                    var option = context.PurchaseOptions.FirstOrDefault(x => x.Id.Equals(purchaseOptionId));
                    if (option != null)
                    {
                        var cartItem = new CartItem
                        {
                            PurchaseOptionId = option.Id,
                            ImageId = option.ParentImage.Id,
                            Name = option.ParentImage.Name,
                            Price = option.Price,
                            Resolution = option.Resolution
                        };
                        items.Add(cartItem);
                        InserCartItems(items);
                        result = true;
                    }
                }
            }
            return result;
        }

        public void RemoveItemFromCart(Guid purchaseOptionId)
        {
            var items = GetCartItems();
            items.RemoveAll(x => x.PurchaseOptionId.Equals(purchaseOptionId));
            InserCartItems(items);
        }

        public int GetCartSize()
        {
            return GetCartItems().Count;
        }

        public List<CartItem> GetCartItems()
        {
            var cart = HttpContext.Current.Session["cart"];
            if (cart != null)
            {
                return (List<CartItem>)cart;
            }
            return new List<CartItem>();
        }

        public void InserCartItems(List<CartItem> items)
        {
            HttpContext.Current.Session["cart"] = items;
        }
    }
}
