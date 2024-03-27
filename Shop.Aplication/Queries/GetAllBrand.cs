using MediatR;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Queries.BrandQueries;

public class GetAllBrand:IRequest<IEnumerable<Brand>>
{

    public int Page { get; set; }
    public int Take { get; set; }

    public GetAllBrand(int page, int take)
    {
        Page = page;
        Take = take;
    }
}
public class HandGetAllBrand:IRequestHandler<GetAllBrand,IEnumerable<Brand>>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandGetAllBrand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Brand>> Handle(GetAllBrand request, CancellationToken cancellationToken)
    {
        try
        {
            var brands = await _unitOfWork.brandRepository.GetAllBrand(request.Page,request.Take);

            return brands;

        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }
    }
}