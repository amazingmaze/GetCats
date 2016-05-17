using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GetCats.Models.Entities;

namespace GetCats.Models.ViewModels
{
    public class ImageDetailViewModel
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public List<OptionBidViewModel> OptionsWithBids { get; set; } 
    }
}