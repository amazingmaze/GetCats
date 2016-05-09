using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GetCats.Models.Entities;

namespace GetCats.Models
{

    public class ImageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public virtual ICollection<PurchaseOption> Options { get; set; } = new List<PurchaseOption>();
    }
}
