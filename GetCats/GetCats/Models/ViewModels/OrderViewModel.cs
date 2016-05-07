using System;
using System.Collections.Generic;
using GetCats.Models.DTO;
using GetCats.Models.Entities;

namespace GetCats.Models.ViewModels
{
    public class OrderViewModel
    {
        public Order.OrderStatus Status { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime StatusChanged { get; set; }
        public List<CartItem> Items { get; set; }
        public int Total { get; set; }
        public int SubTotal { get; set; }
        public int Tax { get; set; }
        public int Shipping { get; set; }
        public PaypalPaymentParams.PaymentCurrency Currency { get; set; }
    }
}