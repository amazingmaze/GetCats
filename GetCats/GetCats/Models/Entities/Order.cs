﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using GetCats.Models.DTO;

namespace GetCats.Models.Entities
{
    public class Order
    {
        public enum OrderStatus
        {
            InPogress,
            Payed,
            Delivered
        }

        public Order()
        {
            Status = OrderStatus.InPogress;
            Id = Guid.NewGuid();
            Created = StatusChanged = DateTime.Now;
            Items = new List<PurchaseOption>();
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        public virtual User User { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime StatusChanged { get; set; }
        public string PaymentId { get; set; }
        public int Total { get; set; }
        public int SubTotal { get; set; }
        public int Tax { get; set; }
        public int Shipping { get; set; }
        public PaypalPaymentParams.PaymentCurrency Currency { get; set; }

        public virtual ICollection<PurchaseOption> Items { get; set; }
    }
}