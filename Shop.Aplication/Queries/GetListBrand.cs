using MediatR;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Queries.BrandQueries;

public class GetListBrand:IRequest<IEnumerable<Brand>>
{

}
public class HandGetListBrand:IRequestHandler<GetListBrand,IEnumerable<Brand>>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandGetListBrand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Brand>> Handle(GetListBrand request, CancellationToken cancellationToken)
    {
        try
        {
            var brands = await _unitOfWork.brandRepository.GetAllAsync();

            return brands is null? Enumerable.Empty<Brand>(): brands;

        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }
    }
}