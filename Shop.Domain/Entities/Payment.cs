using Shop.Domain.Abstraction;
using Shop.Domain.Enum;

namespace Shop.Domain.Entities;

public class Payment:BaseEntity
{
    public Guid Id { get; set; }
    
    public PaymentMethod PaymentMethod { get; set; }
    
    public string? BankCode { get; set; }
    
    public double Amount{ get; set; }
    
    public DateTime PayDate { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid OrderId { get; set; }

    public User? User;
    
    public Order? Order { get; set; }

}