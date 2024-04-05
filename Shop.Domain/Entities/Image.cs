using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class Image : BaseEntity
    {
        public Guid Id { get; set; }
        public string? ImageUrl {  get; set; }
       
        [JsonIgnore]
        public Guid ProductId { get; set; }
        
        [JsonIgnore]
        public Product? Product { get; set; }

        public Image(Guid id, string? imageUrl, Guid productId)
        {
            Id = id;
            ImageUrl = imageUrl;
            ProductId = productId;
        }

      
    }
}
