using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetCats.Models.Entities;

namespace GetCats.Models.DTO
{
    public class OrderListItem
    {
        public Order.OrderStatus Status { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public int Total { get; set; }
        public PaypalPaymentParams.PaymentCurrency Currency { get; set; }
    }
}
