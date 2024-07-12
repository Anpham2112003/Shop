using MediatR;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.BrandCommand;

public class CreateBrandCommand:IRequest<Brand>
{
    public string? Name { get; set; }
    
}

public class HandCreateBrandCommand : IRequestHandler<CreateBrandCommand, Brand>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandCreateBrandCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Brand> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var brand = new Brand()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
            };
            await _unitOfWork.brandRepository.AddAsync(brand);

            await _unitOfWork.SaveChangesAsync();

            return brand;
        }
        catch (Exception )
        {
            throw;
        }
      
    }
}