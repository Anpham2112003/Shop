using Shop.Domain.Abstraction;

namespace Shop.Domain.Entities;

public class Tag:BaseEntity
{
    public Guid Id { get; set; }
    public string? TagName { get; set; }
    public ICollection<ProductTag>? ProductTags { get; } = new List<ProductTag>();
    public ICollection<Product> Products { get; } = new List<Product>();
}