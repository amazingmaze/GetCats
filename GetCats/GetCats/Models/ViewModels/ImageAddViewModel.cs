using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GetCats.Views.Admin.UploadImageValidators;

namespace GetCats.Models.ViewModels
{
    public class ImageAddViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [FileTypes("jpg,jpeg,png")]
        public HttpPostedFileBase File { get; set; }
        public string FileName { get; set; }
        [Required]
        public double BuyOutPrice { get; set; }
    }
}