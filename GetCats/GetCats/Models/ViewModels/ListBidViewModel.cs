using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GetCats.Models.Entities;

namespace GetCats.Models.ViewModels
{
    public class ListBidViewModel
    {
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Bid")]
        public int Bid { get; set; }

        [Display(Name = "Bid id")]
        public Guid BidId { get; set; }

        [Display(Name = "User")]
        public  string Email { get; set; }

        [Display(Name = "Image Resolution")]
        public string Resolution { get; set; }

        [Display(Name = "Image name")]
        public string ImageName { get; set; }

        [Display(Name = "Price")]
        public int Price { get; set; }

        public User Bidder { get; set; }

        public PurchaseOption ImageOption { get; set; }

    }
}