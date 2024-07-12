using System.Text.Json.Serialization;
using Shop.Domain.Abstraction;

namespace Shop.Domain.Entities;

public class ProductTag
{
    public Guid TagId { get; set; }
    public Guid ProductId { get; set; }
    
 
}