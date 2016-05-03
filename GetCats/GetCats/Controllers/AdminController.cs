using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GetCats.Models.ViewModels;

namespace GetCats.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        
    }

}