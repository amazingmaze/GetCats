using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GetCats.Models.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GetCats.Models.ViewModels
{
    public class AdminViewModel
    {
        public string Id { get; set; }
        public string Street { get; set; }
        public string Region { get; set; }
        public string Country { get; set; } 
        public string PostalCode { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public string Permission { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> Statuses => new List<SelectListItem> { new SelectListItem{ Value="0", Text="Inactive" }, new SelectListItem { Value = "1", Text = "Active" } };
        public List<SelectListItem> Permissions => new List<SelectListItem> { new SelectListItem { Value = "0", Text = "User" }, new SelectListItem { Value = "1", Text = "Admin" } };

    }
}