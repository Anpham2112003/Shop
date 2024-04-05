using MediatR;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Queries.ProductQueries;

public class GetProductsByCategoryId:IRequest<PagingResponseModel<List<ProductPreviewResponseModel>>?>
{
    public Guid Id { get; set; }
    public int Page { get; set; }
    public int Take { get; set; }

    public GetProductsByCategoryId(Guid id, int page, int take)
    {
        Id = id;
        Page = page;
        Take = take;
    }
}

public class HandGetProductsByCategoryId:IRequestHandler<GetProductsByCategoryId,PagingResponseModel<List<ProductPreviewResponseModel>>?>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandGetProductsByCategoryId(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagingResponseModel<List<ProductPreviewResponseModel>>?> Handle(GetProductsByCategoryId request, CancellationToken cancellationToken)
    {
        var result = await  _unitOfWork.productRepository.GetProductByCategoryId(request.Id,request.Page,request.Take);
        
        if (!result.Any()) return null;
        
        var total = await  _unitOfWork.productRepository.CountProductByCategoryId(request.Id);
        
        return new PagingResponseModel<List<ProductPreviewResponseModel>>()
        {
            Data = result,
            Message = "success",
            Total = total
        };
    }
}