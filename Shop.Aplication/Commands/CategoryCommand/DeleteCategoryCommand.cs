using MediatR;
using Shop.Infratructure.UnitOfWork;
using Exception = System.Exception;

namespace Shop.Aplication.Commands.CategoryCommand;

public class DeleteCategoryCommand:IRequest<bool>
{
    public Guid Id { get; set; }

    public DeleteCategoryCommand(Guid id)
    {
        Id = id;
    }
}

public class HandDeleteCategoryCommand : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandDeleteCategoryCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var category = await _unitOfWork.categoryRepository.FindByIdAsync(request.Id);

            if (category is null) return false;
        
            _unitOfWork.categoryRepository.Remove(category);
            
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}