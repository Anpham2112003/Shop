﻿using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class Cart : BaseEntity
    {
        public Guid Id {  get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Guid UserId { get; set; }
        
        public User? User { get; set; }

        public Product? Product { get; set; }
       
    }
}
