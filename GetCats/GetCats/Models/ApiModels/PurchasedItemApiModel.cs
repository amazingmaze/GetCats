﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GetCats.Models.Entities;

namespace GetCats.Models.ApiModels
{
    public class PurchasedItemApiModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public PurchaseOption.ImageResolution Resolution { get; set; }
        public int Price { get; set; }
    }
}