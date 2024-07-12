using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Entities;
using Shop.Domain.Ultils;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.CartCommand;

public class CreateCartCommand:IRequest<bool>
{
    public Guid Id = Guid.NewGuid();
    public Guid ProductId { get; set; }
   
    public int Quantity { get; set; }
  

}

public class HandCreateCartCommand:IRequestHandler<CreateCartCommand,bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;
    public HandCreateCartCommand(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
    }

    public async Task<bool> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var UserId = _contextAccessor.HttpContext!.User.GetIdFromClaim();
            
            var product = await _unitOfWork.productRepository.Check(request.ProductId);

            if ( product is false) return false;

            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                UserId = Guid.Parse(UserId),
            };
            
            
            await _unitOfWork.cartRepository.AddAsync(cart);
            
            await _unitOfWork.SaveChangesAsync();
            
            return true;
        }
        catch (Exception )
        {
            throw ;
        }
    }
}