using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetCats.Models.Entities
{
    public class Image
    {
        public Image()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } //describing name of image
        [Required]
        public string FileName { get; set; } //unique name of file
        public virtual ICollection<PurchaseOption> Options { get; set; } = new List<PurchaseOption>();
    }
}
