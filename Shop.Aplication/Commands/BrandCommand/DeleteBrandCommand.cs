using MediatR;
using Shop.Infratructure.UnitOfWork;
using Exception = System.Exception;

namespace Shop.Aplication.Commands.BrandCommand;

public class DeleteBrandCommand:IRequest<DeleteBrandCommand?>
{
    public Guid Id { get; set; }
}

public class HandDeleteBrandCommand : IRequestHandler<DeleteBrandCommand, DeleteBrandCommand?>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandDeleteBrandCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteBrandCommand?> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var brand = await _unitOfWork.brandRepository.FindByIdAsync(request.Id);

            if (brand is null) return null;
        
            _unitOfWork.brandRepository.Remove(brand);
        
            await _unitOfWork.SaveChangesAsync();
            
            return request;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }
}