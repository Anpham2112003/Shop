using MediatR;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.OrderCommand;

public class DeleteOrderCommand:IRequest<bool>
{
    public Guid Id { get; set; }

    public DeleteOrderCommand(Guid id)
    {
        Id = id;
    }
}

public class HandDeleteOrderCommand:IRequestHandler<DeleteOrderCommand,bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandDeleteOrderCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _unitOfWork.orderRepository.FindByIdAsync(request.Id);

            if (order is null || order.OrderState ) return false;
        
            _unitOfWork.orderRepository.Remove(order);
            
            await _unitOfWork.SaveChangesAsync();

            var product = await _unitOfWork.productRepository.FindByIdAsync(order.ProductId);

            if (product is not null)
            {
                product.Quantity += order.Quantity;
                await _unitOfWork.SaveChangesAsync();
            }

            return true;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        
    }
}