using System.Collections.Generic;

namespace GetCats.Models.DTO
{
    public class CartResult
    {
        public decimal TotalPrice { get; set; }
        public int ItemsInCart { get; set; }
        public List<CartItem> Items { get; set; }
    }
}
