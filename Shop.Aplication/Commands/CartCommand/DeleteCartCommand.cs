using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Ultils;
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
    private readonly IHttpContextAccessor _contextAccessor;
    public HandDeleteCartCommand(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    public async Task<bool> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var UserId = Guid.Parse(_contextAccessor.HttpContext!.User.GetIdFromClaim());

            var cart = await _unitOfWork.cartRepository.FindByIdAsync(request.Id);

            if (cart is null || cart.UserId.Equals(UserId) is false) return false;
        
            _unitOfWork.cartRepository.Remove(cart);
        
            await _unitOfWork.SaveChangesAsync();
        
            return true;
        }
        catch (Exception )
        {
            throw ;
        }
       
    }
}