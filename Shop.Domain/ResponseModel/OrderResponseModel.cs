using Shop.Domain.Enums;

namespace Shop.Domain.ResponseModel;

public class OrderResponseModel
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; }
    public double Amount { get; set;}
    public OrderState OrderState {  get; set; }
    public int Quantity {  get; set;}   
    public string? ImageUrl {  get; set; }

}