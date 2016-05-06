using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public decimal Price { get; set; }
    }
}
