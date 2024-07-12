using MediatR;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;
using Exception = System.Exception;

namespace Shop.Aplication.Commands.CategoryCommand;

public class CreateCategoryCommand:IRequest<Category>
{
    public string? Name { get; set; }
}

public class HandCreateCategoryCommand : IRequestHandler<CreateCategoryCommand, Category>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandCreateCategoryCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var category = new Category()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
            };
            await  _unitOfWork.categoryRepository.AddAsync(category);
            
            await _unitOfWork.SaveChangesAsync();
            
            return category;
        }
        catch (Exception )
        {
            throw ;
        }
         
    }
}