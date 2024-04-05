using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.ProductCommand;

public class UpdateProductCommand:IRequest<UpdateProductCommand?>
{
    public Guid Id =Guid.NewGuid();

    public string? Name{get;set;}
        
    public double Price{get;set;}
        
    public int Quantity{get;set;}
    
    public string? Description { get;set;}

    public Guid? BrandId { get;set;}
    
    public  DateTime UpdatedAt =DateTime.UtcNow;
    
}

public class HandUpdateProductCommand:IRequestHandler<UpdateProductCommand,UpdateProductCommand?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public HandUpdateProductCommand(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UpdateProductCommand?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product= await _unitOfWork.productRepository.FindByIdAsync(request.Id);
            
            var brand = await _unitOfWork.brandRepository.FindByIdAsync(request.Id);
            
            if (product is null || brand is null) return null;

            _mapper.Map(request, product);

            await _unitOfWork.SaveChangesAsync();

            return request;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        
    }
}