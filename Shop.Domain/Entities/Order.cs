using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Shop.Domain.Enum;

namespace Shop.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid Id { get; set; }
        
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set;}
        public string? ImageUrl { get; set; }
        
        public Guid UserId { get; set; }
        
        public Guid ProductId { get; set; }

        public bool OrderState { get; set; }
        
        public PaymentMethod PaymentMethod { get; set; }
        
        [JsonIgnore]
        public Product? Product { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
        
        
        public Payment? Payment { get; set; }
        
        public Ship? Ship { get; set; }

    }
}
