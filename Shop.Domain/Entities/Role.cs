using System.Text.Json.Serialization;
using Shop.Domain.Abstraction;

namespace Shop.Domain.Entities;

public class Role:BaseEntity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    
    [JsonIgnore]
    public ICollection<User>? Users;

    public Role(Guid id, string? name)
    {
        Id = id;
        Name = name;
    }
}