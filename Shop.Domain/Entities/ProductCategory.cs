using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class ProductCategory : BaseEntity
    {
        public Guid Id {  get; set; }
        public Guid ProductId { get; set; }
        public Guid CategoryId { get; set; }
        public Product? product { get; set; }
        public Category? category { get; set; }

    }
}
