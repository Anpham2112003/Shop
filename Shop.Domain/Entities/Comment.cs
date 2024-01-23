using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public string? UserName {  get; set; }
        public string? Content {  get; set; }
        public int Rate {  get; set; }
        public User? User { get; set; }
        public Product? Product { get; set; }

       
    }
}
