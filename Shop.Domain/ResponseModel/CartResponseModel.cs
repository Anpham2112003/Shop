using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ResponseModel
{
    public class CartResponseModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }
        public string? Image {  get; set; }
        public bool IsDiscount {  get; set; }
        public double? PriceDiscount { get; set; }
        public double Amount { get; set; }
    }
}
