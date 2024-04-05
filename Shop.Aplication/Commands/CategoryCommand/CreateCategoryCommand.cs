using MediatR;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;
using Exception = System.Exception;

namespace Shop.Aplication.Commands.CategoryCommand;

public class CreateCategoryCommand:IRequest<CreateCategoryCommand>
{
    public Guid Id =Guid.NewGuid();
    public string? Name { get; set; }
}

public class HandCreateCategoryCommand : IRequestHandler<CreateCategoryCommand, CreateCategoryCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandCreateCategoryCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateCategoryCommand> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await  _unitOfWork.categoryRepository.AddAsync(new Category() { Id = request.Id, Name = request.Name });
            
            await _unitOfWork.SaveChangesAsync();
            
            return request;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
         
    }
}