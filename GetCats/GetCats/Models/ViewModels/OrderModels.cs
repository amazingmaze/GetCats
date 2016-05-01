using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetCats.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Decimal Total { get; set; }
        public string Customer { get; set; }
        public string OrderStatus { get; set; }

        public virtual ICollection<OrderItems> Items { get; set; }

    }

    public class OrderItems
    {
        public int Id { get; set; }
        public int Quanity { get; set; }
        public int OrderId { get; set; }

    }
}