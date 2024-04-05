using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Domain.Enum;
using Shop.Infratructure.AplicatonDBcontext;
using Shop.Infratructure.UnitOfWork;


namespace Shop.Aplication.Commands.OrderCommand;

public class CreateOrderCommand:IRequest<bool>
{
    public Guid Id =Guid.NewGuid();
    
    public string? Name { get; set; }
    public int Quantity { get; set; }
    
    public double TotalPrice { get; set;}
    
    public PaymentMethod Method { get; set; }
    public string? ImageUrl { get; set; }
    
    public Guid UserId { get; set; }
        
    public Guid ProductId { get; set; }
}

public class HandCreateOrderCommand:IRequestHandler<CreateOrderCommand,bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    private readonly ApplicationDbContext _context;

    public HandCreateOrderCommand(IUnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext context)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _context = context;
    }

    public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
       
        try
        {
            var product = await _unitOfWork.productRepository.FindByIdAsync(request.ProductId);
            // find product
            if (product is null) return false;

            var quantity = product.Quantity - request.Quantity;
               
            if (quantity < 0)
            {
                request.Quantity = product.Quantity;
            }
            
            product.Quantity = quantity;
            

            request.TotalPrice = product.Price * request.Quantity;
            
            var order = _mapper.Map<Order>(request);
        
           await _unitOfWork.orderRepository.AddAsync(order);
           
           await _unitOfWork.SaveChangesAsync();
           
           
           return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;

        }
        
        
    }
}