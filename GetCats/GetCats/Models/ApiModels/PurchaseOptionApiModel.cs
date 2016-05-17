using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GetCats.Models.Entities;
using GetCats.Models.ViewModels;

namespace GetCats.Models.ApiModels
{
    public class PurchaseOptionApiModel
    {
        
        public string Id { get; set; }
        public PurchaseOption.ImageResolution Resolution { get; set; }
        public int Price { get; set; }
        public OptionBidViewModel Bid { get; set; }
    }
}