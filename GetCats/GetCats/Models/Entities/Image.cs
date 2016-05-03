using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string Name { get; set; }
        public virtual ICollection<PurchaseOption> Options { get; set; }
    }
}
