using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Shop.Domain.Entities;
using Shop.Infratructure.AplicatonDBcontext;
using Shop.Infratructure.Services.Aws3Service;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.ProductCommand;

public class CreateProductCommand:IRequest<CreateProductCommand>
{
    public Guid Id =Guid.NewGuid();

    public string? Name{get;set;}
        
    public double Price{get;set;}
        
    public int Quantity{get;set;}
    
    public string? Description { get;set;}

    public Guid BrandId { get;set;}
    
    public Guid CategoryId { get; set; }
    
    public  DateTime CreatedAt =DateTime.UtcNow;
    
    public IFormFile? Image { get; set; }

    public CreateProductCommand()
    {
    }
}

public class HandCreateProductCommand:IRequestHandler<CreateProductCommand,CreateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAwsSevice _awsSevice;
    private readonly IConfiguration _configuration;
    public HandCreateProductCommand(IUnitOfWork unitOfWork, IMapper mapper, IAwsSevice awsSevice, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _awsSevice = awsSevice;
        _configuration = configuration;
    }

    public async Task<CreateProductCommand> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
      
       
        try
        {
         
            var random = new Random();
            var product = _mapper.Map<Product>(request);
            
            StringBuilder imageKey = new StringBuilder();
            imageKey.Append(request.Id);
            imageKey.Append(random.NextInt64(1,1000000000000));
            imageKey.Append("imageProduct.");
            imageKey.Append(Path.GetExtension(request.Image.FileName));
            
            
            await _awsSevice.Upload(request.Image, _configuration["AWS:Bucket"], imageKey.ToString(), cancellationToken);
            
            await _unitOfWork.productRepository.AddAsync(product);
            
            await _unitOfWork.imageRepository.AddAsync(new Image(Guid.NewGuid(),imageKey.ToString(),request.Id ));
            
            await _unitOfWork.SaveChangesAsync();
            
            

            

            return request;
        }
        catch (Exception e)
        {
            
           throw new Exception(e.Message);
        }
       

    }
}