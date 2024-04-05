namespace Shop.Domain.ResponseModel;

public class UsersResponseModel
{
    public Guid Id { get; set; }
    
    public string? FullName { get; set; }
    
    public string? Email { get; set; }
    
    public string? Role { get; set; }
    
    public bool IsActive { get; set; }
    
    public bool IsDelete { get; set; }
    
    public DateTime DeletedAt { get; set; }
}