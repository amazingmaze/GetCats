using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GetCats.Models.Entities
{
    public class Order
    {
        public enum OrderStatus
        {
            InPogress,
            Payed,
            Delivered
        }

        public Order()
        {
            Status = OrderStatus.InPogress;
            OrderDate = DateTime.UtcNow;
            Id = Guid.NewGuid();
            Created = StatusChanged = DateTime.Now;
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public virtual User User { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime StatusChanged { get; set; }
        public string PaymentId { get; set; }

        public virtual ICollection<PurchaseOption> Items { get; set; } = new List<PurchaseOption>();
    }
}