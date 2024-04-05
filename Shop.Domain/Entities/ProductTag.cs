using System.Text.Json.Serialization;
using Shop.Domain.Abstraction;

namespace Shop.Domain.Entities;

public class ProductTag:BaseEntity
{
    public Guid Id { get; set; }
    public Guid TagId { get; set; }
    public Guid ProductId { get; set; }
    
    [JsonIgnore]
    public Tag? Tag { get; set; }
    
    [JsonIgnore]
    public Product? Product { get; set; }
}