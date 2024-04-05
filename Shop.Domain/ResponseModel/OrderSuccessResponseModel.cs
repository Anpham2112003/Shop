namespace Shop.Domain.ResponseModel;

public class OrderSuccessResponseModel
{
    public Guid OrderId { get; set; }
    public string? ProductName { get; set; }
    
    public double Amount { get; set;}
    
    public string? ImageUrl { get; set; }
    
    public bool OrderState { get; set; }
}