using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Shop.Domain.Enum;
using Shop.Domain.Interfaces;
using Shop.Domain.Enums;

namespace Shop.Domain.Entities
{
    public class Order : BaseEntity,ISoftDelete
    {
        public Guid Id { get; set; }
        
        public int Quantity { get; set; }
        public double TotalPrice { get; set;}
        
        public Guid UserId { get; set; }
        
        public Guid ProductId { get; set; }

        public OrderState OrderState { get; set; }
        
        public PaymentMethod PaymentMethod { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }
        public Product? Product { get; set; }

        
        public User? User { get; set; }
        
        
        public Payment? Payment { get; set; }
        
        public Ship? Ship { get; set; }

    }
}
