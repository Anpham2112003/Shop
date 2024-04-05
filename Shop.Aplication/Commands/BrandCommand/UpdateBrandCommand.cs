using MediatR;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.BrandCommand;

public class UpdateBrandCommand:IRequest<UpdateBrandCommand?>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class HandUpdateBrandCommand : IRequestHandler<UpdateBrandCommand,UpdateBrandCommand?>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandUpdateBrandCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateBrandCommand?> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var brand = await _unitOfWork.brandRepository.GetByIdAsync(request.Id);

            if (brand is null) return null;

            brand.Name = request.Name;
        
            await _unitOfWork.SaveChangesAsync();

            return request;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
       
    }
}