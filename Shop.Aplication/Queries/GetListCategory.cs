using MediatR;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Queries.CategoryQueries;

public  class GetListCategory:IRequest<IEnumerable<Category>?>
{
    
}

public class HandGetListCategory:IRequestHandler<GetListCategory,IEnumerable<Category>?>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandGetListCategory(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Category>?> Handle(GetListCategory request, CancellationToken cancellationToken)
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