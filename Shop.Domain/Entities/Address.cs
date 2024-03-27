using Shop.Domain.Abstraction;
using Shop.Domain.Entities;

namespace Shop.Domain.Interfaces;

public class Address:BaseEntity
{
    public Guid Id { get; set; }
    
    public string? StreetAddress { get; set; }
    public string? Commune { get; set; }
    public string? District { get; set; }
    public string? City { get; set; }
    
    public Guid UserId { get; set; }
    
    
    public User? User { get; set; }
}