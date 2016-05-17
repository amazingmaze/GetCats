using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GetCats.Views.Admin.UploadImageValidators;
using GetCats.Models.Entities;

namespace GetCats.Models.ViewModels
{
    public class ImageAddViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [FileTypes("jpg,jpeg,png")]
        public HttpPostedFileBase File { get; set; }
        public string FileName { get; set; }
        //public PurchaseOption Options { get; set; }
        public PurchaseOption LowRes { get; set; }
        public PurchaseOption HighRes { get; set; }
        public PurchaseOption MaxRes { get; set; }
    }
}