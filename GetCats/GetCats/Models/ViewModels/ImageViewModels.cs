using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GetCats.Models
{

    public class ImageViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Src { get; set; }
        public double BuyOutPrice { get; set; }
        public double CurrentBid { get; set; }
    }
}
