using Shop.Domain.Abstraction;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class Product:BaseEntity,ICreate,ISoftDelete,IModify
    {

        public  Guid Id { get; set; }

        public string? Name{get;set;}
        
        public double Price{get;set;}
        public int Quantity{get;set;}

        public bool IsDiscount { get;set;}
        public double PriceDiscount{get;set;}
        public string? Description { get;set;}

        public Guid? BrandId { get;set;}

        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime UpdatedAt { get; set; }






        public Brand? Brand { get;set;}
        


        public List<Order>? Orders { get; set; }

      
        public Image? Image { get; set; }
        
        [JsonIgnore]
        public ICollection<Cart>? Carts { get; set;} 
        
        [JsonIgnore]

        public ICollection<Comment>? Comments { get; set; }

        public ICollection<Tag> Tags {  get; } = new List<Tag>();
        public ICollection<ProductTag> ProductTags { get; } = new List<ProductTag>();
        public ICollection<Category> Categories { get; } = new List<Category>();
        public ICollection<ProductCategory> ProductCategories { get; } = new List<ProductCategory>();

      
    }
}
