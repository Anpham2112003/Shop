using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class Image : BaseEntity
    {
        public Guid Id { get; set; }
        public string? ImageUrl {  get; set; }
        public string? ColorImage {  get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

    }
}
