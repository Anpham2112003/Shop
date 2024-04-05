using Shop.Domain.Entities;

namespace Shop.Domain.ResponseModel;

public class ProductPreviewResponseModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public Image? Image { get; set; }
}