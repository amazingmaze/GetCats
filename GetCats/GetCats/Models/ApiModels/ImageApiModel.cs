using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetCats.Models.ApiModels
{
    public class ImageApiModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public PurchaseOptionApiModel[] Options { get; set; }
    }
}