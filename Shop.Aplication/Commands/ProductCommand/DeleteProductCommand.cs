using MediatR;
using Shop.Infratructure.AplicatonDBcontext;
using Shop.Infratructure.UnitOfWork;
using Exception = System.Exception;

namespace Shop.Aplication.Commands.ProductCommand;

public class DeleteProductCommand:IRequest<bool>
{
    public Guid Id { get; set; }

    public DeleteProductCommand(Guid id)
    {
        Id = id;
    }
}

public class HandDeleteProductCommand:IRequestHandler<DeleteProductCommand,bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _dbContext;
    
    public HandDeleteProductCommand(IUnitOfWork unitOfWork, ApplicationDbContext dbContext)
    {
        _unitOfWork = unitOfWork;
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            
            var product = await _unitOfWork.productRepository.FindByIdAsync(request.Id);

            if (product is null) return false;

            product.IsDeleted = true;
            product.DeletedAt = DateTime.UtcNow;

            _unitOfWork.productRepository.Update(product);
            
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        catch (Exception )
        {
            throw ;
        }
        
        
    }
}

