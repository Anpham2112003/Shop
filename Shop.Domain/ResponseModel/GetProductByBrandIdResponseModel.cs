using Shop.Domain.Entities;

namespace Shop.Domain.ResponseModel;

public class GetProductByBrandIdResponseModel
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    
    public double Price { get; set; }
    
    public int Quantity { get; set; }
    
    public Image? Image { get; set; }
    
    
}