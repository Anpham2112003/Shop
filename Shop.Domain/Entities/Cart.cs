using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public Guid Id {  get; set; }
        public Guid ProductId { get; set; }
        public string? ProductName {  get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public string? ImageUrl {  get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
       
    }
}
