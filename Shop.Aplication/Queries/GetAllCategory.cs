using MediatR;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Queries.CategoryQueries;

public  class GetAllCategory:IRequest<IEnumerable<Category>?>
{
    
}

public class HandGetAllCategory:IRequestHandler<GetAllCategory,IEnumerable<Category>?>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandGetAllCategory(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Category>?> Handle(GetAllCategory request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _unitOfWork.categoryRepository.GetAllAsync();
            
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
       

        
    }
}