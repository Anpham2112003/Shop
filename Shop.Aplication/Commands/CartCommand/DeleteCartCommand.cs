using MediatR;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.CartCommand;

public class DeleteCartCommand:IRequest<bool>
{
    public Guid Id { get; set; }

    public DeleteCartCommand(Guid id)
    {
        Id = id;
    }
}

public class HandDeleteCartCommand:IRequestHandler<DeleteCartCommand,bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandDeleteCartCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var cart = await _unitOfWork.cartRepository.FindByIdAsync(request.Id);

            if (cart is null) return false;
        
            _unitOfWork.cartRepository.Remove(cart);
        
            await _unitOfWork.SaveChangesAsync();
        
            return true;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
       
    }
}