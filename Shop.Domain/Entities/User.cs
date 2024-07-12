using Shop.Domain.Abstraction;
using Shop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shop.Domain.Entities
{
    public class User : BaseEntity,ICreate,ISoftDelete,IModify
    {
        public Guid Id { get; set; }
        public string? FistName { get; set; }
        public string? LastName {  get; set; }
        public string? FullName { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        
        public bool IsActive { get; set; }
        public Guid RoleId { get; set; }
     
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get;set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }

        [JsonIgnore]
        public  Role?  Role{  get; set; }
        
        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; }
        
        [JsonIgnore]
        public ICollection<Comment>? Comments { get; set; }
        
        [JsonIgnore]
        public ICollection<Cart>? Carts { get; set; }
        
       

        [JsonIgnore]
        public ICollection<Payment>? Payments { get; set; }

        public ICollection<Address>? Addresses { get; set; }


    }
}
