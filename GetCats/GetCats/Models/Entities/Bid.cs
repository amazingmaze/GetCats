using System;
using System.ComponentModel.DataAnnotations;
namespace GetCats.Models.Entities
{
    public class Bid
    {
        public enum BidStatus
        {
            Approved,
            Denied,
            Initial
        }

        public Bid()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        public virtual User Bidder { get; set; }
        [Required]
        public BidStatus Status { get; set; } = BidStatus.Initial;
        [Required]
        public int Price { get; set; }
        [Required]
        public virtual PurchaseOption ImageOption { get; set; }

    }
}
