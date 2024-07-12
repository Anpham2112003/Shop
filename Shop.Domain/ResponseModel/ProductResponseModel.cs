using Shop.Domain.Entities;

namespace Shop.Domain.ResponseModel;

public class ProductResponseModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public bool IsDiscount {  get; set; }
    public double PriceDiscount {  get; set; }
    public Image? Image { get; set; }
    public Brand? Brand { get; set; }

    public ICollection<Category>? Categories { get; set; }
    public ICollection<Tag>? Tags { get; set; }
    

}