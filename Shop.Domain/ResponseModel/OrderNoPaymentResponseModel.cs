namespace Shop.Domain.ResponseModel;

public class OrderNoPaymentResponseModel
{
    public Guid Id { get; set; }
   
    public int Quantity { get; set; }
    
    public double TotalPrice { get; set;}
    
    public string? ImageUrl { get; set; }
}