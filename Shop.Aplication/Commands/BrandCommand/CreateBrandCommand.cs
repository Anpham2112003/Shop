using MediatR;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.BrandCommand;

public class CreateBrandCommand:IRequest<CreateBrandCommand>
{
    public  Guid Id => Guid.NewGuid();
    public string? Name { get; set; }
    
}

public class HandCreateBrandCommand : IRequestHandler<CreateBrandCommand, CreateBrandCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandCreateBrandCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateBrandCommand> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.brandRepository.AddAsync(new Brand() { Id = request.Id, Name = request.Name });
            await _unitOfWork.SaveChangesAsync();
            return request;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
      
    }
}