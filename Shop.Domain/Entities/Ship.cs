using System.Text.Json.Serialization;
using Shop.Domain.Abstraction;
using Shop.Domain.Enums;

namespace Shop.Domain.Entities;

public class Ship:BaseEntity
{
    public Guid Id { get; set; }

    public ShipState State { get; set; }
    
    public string? StreetAddress { get; set; }
    
    public string? Commune { get; set; }
    
    public string? District { get; set; }
    
    public string? City { get; set; }
    
    public Guid OrderId { get; set; }
    
    [JsonIgnore]
    public Order? Order { get; set; }
}