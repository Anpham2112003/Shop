using MediatR;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.CategoryCommand;

public class UpdateCategoryCommand:IRequest<UpdateCategoryCommand?>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class HandUpdateCategoryCommand : IRequestHandler<UpdateCategoryCommand,UpdateCategoryCommand?>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandUpdateCategoryCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateCategoryCommand?> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var category = await _unitOfWork.categoryRepository.FindByIdAsync(request.Id);

            if (category is null) return null;

            category.Name = request.Name;

            await _unitOfWork.SaveChangesAsync();

            return request;
        }
        catch (Exception )
        {
            throw;
        }

    }
}