using AutoMapper;
using MediatR;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.CartCommand;

public class CreateCartCommand:IRequest<bool>
{
    public Guid Id = Guid.NewGuid();
    public Guid ProductId { get; set; }
    public string? ProductName {  get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
    public string? ImageUrl {  get; set; }
    public Guid UserId { get; set; }
}

public class HandCreateCartCommand:IRequestHandler<CreateCartCommand,bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public HandCreateCartCommand(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _unitOfWork.userRepository.Check(request.UserId);
            
            var product = await _unitOfWork.productRepository.Check(request.ProductId);

            if (user is false || product is false) return false;
            
            var cart = _mapper.Map<Cart>(request);
            
            
            await _unitOfWork.cartRepository.AddAsync(cart);
            
            await _unitOfWork.SaveChangesAsync();
            
            return true;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}