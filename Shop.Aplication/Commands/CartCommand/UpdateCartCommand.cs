using MediatR;

namespace Shop.Aplication.Commands.CartCommand;

public class UpdateCartCommand:IRequest<bool>
{
    public Guid Id { get; set; }
    
    public int Quantity { get; set; }
    
    public double TotalPrice { get; set; }
}