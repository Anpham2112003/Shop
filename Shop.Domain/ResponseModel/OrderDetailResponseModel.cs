using Shop.Domain.Entities;
using Shop.Domain.Enum;
using Shop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ResponseModel
{
    public class OrderDetailResponseModel
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public double Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public OrderState OrderState { get; set; }
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; }
        public Ship? Ship { get; set; }
    }
}
