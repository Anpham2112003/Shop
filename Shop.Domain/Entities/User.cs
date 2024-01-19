﻿using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class User : BaseEntity
    {
        public Guid Id { get; set; }
        public string? FistName { get; set; }
        public string? LastName {  get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role {  get; set; }  
        public string? Refreshtoken {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get;set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
