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
        
        public double Price{get;set;}
        
        public int Quantity{get;set;}
        public string? Description { get;set;}

        public Guid? BrandId { get;set;}
        public Brand? Brand { get;set;}
        public bool IsSale {  get;set;}
        public float? DisCount {  get;set;}
        public double? PriceDisCount {  get;set;}
        public  DateTime CreatedAt { get;set;}

        public DateTime? UpdatedAt { get;set;}

        public bool IsDeleted { get;set;}

        public DateTime? DeletedAt {  get;set;}  

        public ICollection<ProductCategory>? ProductCategories { get; set;}
        public ICollection<Image>? Images { get; set; }
        public ICollection<Cart>? Carts { get; set;} 

        public ICollection<Comment>? Comments { get; set; }
       

    }
}
