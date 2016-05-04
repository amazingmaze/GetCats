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
        public void AddItemToCart(Guid purchaseOptionId)
        {
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
                    }
                }
                InserCartItems(items);
            }
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
            DemoInsertCartItems(); //TODO: Ta bort när add to cart är fixat korrekt
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

        public void DemoInsertCartItems() //TODO: Ta bort när add to cart är fixat korrekt
        {
            if (HttpContext.Current.Session["cart"] != null) return;
            var newCart = new List<CartItem>
            {
                new CartItem { PurchaseOptionId = Guid.NewGuid(), ImageId = Guid.NewGuid(), Resolution = PurchaseOption.ImageResolution.High, Name = "Lolcat1", Price = 512 },
                new CartItem { PurchaseOptionId = Guid.NewGuid(), ImageId = Guid.NewGuid(), Resolution = PurchaseOption.ImageResolution.Low, Name = "Some bunch of cats", Price = 113 },
                new CartItem { PurchaseOptionId = Guid.NewGuid(), ImageId = Guid.NewGuid(), Resolution = PurchaseOption.ImageResolution.Max, Name = "FUNNY CAT!", Price = 98 }
            };
            InserCartItems(newCart);
        }
    }
}
