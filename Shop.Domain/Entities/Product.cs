using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class Product:BaseEntity
    {

        public  Guid Id { get; set; }

        public string? Name{get;set;}
        
        public double Price{get;set;}
        
        public int Quantity{get;set;}
        public string? Description { get;set;}

        public Guid? BrandId { get;set;}
        
        public Guid? CategoryId { get; set; }
        public  DateTime CreatedAt { get;set;}

        public DateTime? UpdatedAt { get;set;}
        
        public Brand? Brand { get;set;}
        
        public Category? Category { get; set; }

        public Order? Order { get; set; }

        [JsonIgnore]
        public Image? Image { get; set; }
        
        [JsonIgnore]
        public ICollection<Cart>? Carts { get; set;} 
        
        [JsonIgnore]

        public ICollection<Comment>? Comments { get; set; }
        
        [JsonIgnore]
        public ICollection<ProductTag>? ProductTags { get; set; }
        
        
       
       

    }
}
