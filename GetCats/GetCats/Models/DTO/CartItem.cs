﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GetCats.Models.Entities;

namespace GetCats.Models.DTO
{
    public class CartItem
    {
        public Guid PurchaseOptionId;
        public Guid ImageId;
        public string Name { get; set; }
        public decimal Price { get; set; }
        public PurchaseOption.ImageResolution Resolution { get; set; }
    }
}