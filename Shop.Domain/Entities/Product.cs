

using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class Product:BaseEntity
    {

        public  Guid Id { get; set; }

        public string? Name{get;set;}
        
        public decimal Price{get;set;}
        
        public string? Decriptions{get;set;}
        
        public int Quantity{get;set;}
        public  DateTime CreatedAt { get;set;}

        public DateTime? UpdatedAt { get;set;}

        public bool IsDeleted { get;set;}

        public DateTime? DeletedAt {  get;set;}  
    }
}
