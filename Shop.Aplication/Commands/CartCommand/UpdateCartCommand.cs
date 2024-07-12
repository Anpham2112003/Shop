using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Ultils;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.CartCommand;

public class UpdateCartCommand:IRequest<bool>
{
    public Guid Id { get; set; }
    
    public int Quantity { get; set; }
    
    
}

public class HandUpdateCartCommand : IRequestHandler<UpdateCartCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public HandUpdateCartCommand(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    public async Task<bool> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var cart = await _unitOfWork.cartRepository.FindByIdAsync(request.Id);

            var UserId = Guid.Parse(_contextAccessor.HttpContext!.User.GetIdFromClaim());

            if (cart is null || cart.UserId != UserId) return false;

            cart.Quantity = request.Quantity;

            _unitOfWork.cartRepository.Update(cart);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
