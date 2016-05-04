using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;

namespace GetCats.Models.Entities
{
    public class PurchaseOption
    {
        public PurchaseOption()
        {
            Id = Guid.NewGuid();
        }

        public enum ImageResolution
        {
            Low,
            High,
            Max
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        public ImageResolution Resolution { get; set; }
        [Required]
        public decimal Price { get; set; }
        public ICollection<Bid> Bids { get; set; }
        public virtual Image ParentImage { get; set; }
    }
}
