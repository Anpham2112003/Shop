using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Enum;
using Shop.Domain.Ultils;
using Shop.Infratructure.AplicatonDBcontext;
using Shop.Infratructure.UnitOfWork;


namespace Shop.Aplication.Commands.OrderCommand;

public class CreateOrderCommand:IRequest<Order?>
{
    public Guid Id =Guid.NewGuid();
    
    public int Quantity { get; set; }
    
    public PaymentMethod Method { get; set; }
    
    public Guid ProductId { get; set; }

    public Guid AddressId {  get; set; }
}

public class HandCreateOrderCommand:IRequestHandler<CreateOrderCommand,Order?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;

    public HandCreateOrderCommand(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
    }

    public async Task<Order?> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
       var transaction =await _unitOfWork.StartTransation();
        try
        {
            var UserId = Guid.Parse(_contextAccessor.HttpContext!.User.GetIdFromClaim());

            var product = await _unitOfWork.productRepository.FindByIdAsync(request.ProductId);
            // find product
            var address = await _unitOfWork.addressRepository.FindByIdAsync(request.AddressId);
           

            if (product is null || address is null) return null;

            var quantity = product.Quantity - request.Quantity;
               
            if (quantity < 0)
            {
                return null;
            }

            var order = new Order
            {
                Id = Guid.NewGuid(),
                Quantity = request.Quantity,
                OrderState = request.Method.Equals(PaymentMethod.Cod)? Domain.Enums.OrderState.PaymentOnDelivery:Domain.Enums.OrderState.WaitPayment,
                PaymentMethod = request.Method,
                ProductId = request.ProductId,
                TotalPrice = product.IsDiscount ?   product.PriceDiscount * request.Quantity : product.Price * request.Quantity,
                UserId = UserId,
            };

            var ship = new Ship
            {
                Id = Guid.NewGuid(),
                City = address.City,
                Commune = address.Commune,
                District = address.District,
                OrderId = order.Id,
                StreetAddress = address.StreetAddress,
                State = Domain.Enums.ShipState.Waiting,
            };
    
        
           await _unitOfWork.orderRepository.AddAsync(order);

           await _unitOfWork.shipRepository.AddAsync(ship);

           
           await _unitOfWork.SaveChangesAsync();
           
           await transaction.CommitAsync();
           
           return order;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            Console.WriteLine(e);
            throw;

        }
        
        
    }
}