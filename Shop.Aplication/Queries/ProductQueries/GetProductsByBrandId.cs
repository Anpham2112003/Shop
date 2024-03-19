using MediatR;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Queries.ProductQueries;

public class GetProductsByBrandId:IRequest<PagingResponseModel<IEnumerable<GetProductByBrandIdResponseModel>>?>
{
    public Guid Id { get; set; }
    public int Page { get; set; }
    public int Take { get; set; }

    public GetProductsByBrandId(Guid id, int page, int take)
    {
        Id = id;
        Page = page;
        Take = take;
    }
}

public class HandGetProductByBrandId:IRequestHandler<GetProductsByBrandId,PagingResponseModel<IEnumerable<GetProductByBrandIdResponseModel>>?>
{
    private readonly IUnitOfWork _unitOfWork ;

    public HandGetProductByBrandId(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagingResponseModel<IEnumerable<GetProductByBrandIdResponseModel>>?> Handle(GetProductsByBrandId request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.productRepository.GetProductByBrandId(request.Id, request.Page, request.Take);
        var total = await _unitOfWork.productRepository.CountProductByBrandId(request.Id); 
        if ( result.Any())
        {
            return new PagingResponseModel<IEnumerable<GetProductByBrandIdResponseModel>>()
            {
                Message = "success",
                Total = total ,
                Data = result
            };
        }
        
        return null;

    }
}