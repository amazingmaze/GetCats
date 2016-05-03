using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GetCats.Models.Entities
{
    public class Order
    {
        /**
            Andreas Svensson 
        */
        public enum OrderStatus
        {
            InPogress,
            Completed
        }

        public Order()
        {
            Status = OrderStatus.InPogress;
            OrderDate = DateTime.UtcNow;
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public virtual User User { get; set; }
        [Required]
        public OrderStatus Status { get; set; }

        public virtual ICollection<PurchaseOption> Items { get; set; } = new List<PurchaseOption>();
    }
}