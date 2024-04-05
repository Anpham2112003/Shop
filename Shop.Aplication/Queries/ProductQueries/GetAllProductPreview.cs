using MediatR;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Queries.ProductQueries;

public class GetAllProductPreview:IRequest<PagingResponseModel<List<ProductPreviewResponseModel>>?>
{
    public int Page { get; set; }
    public int Size { get; set; }

    public GetAllProductPreview(int page, int size)
    {
        Page = page;
        Size = size;
    }
}

public class HandGetAllProductPreview:IRequestHandler<GetAllProductPreview,PagingResponseModel<List<ProductPreviewResponseModel>>?>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandGetAllProductPreview(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagingResponseModel<List<ProductPreviewResponseModel>>?> Handle(GetAllProductPreview request, CancellationToken cancellationToken)
    {
        try
        {
            var products = 
                await _unitOfWork.productRepository
                    .GetProductPreview(request.Page, request.Size);
            var total = await _unitOfWork.productRepository.Count();

            if (  products.Any())
            {
                return new PagingResponseModel<List<ProductPreviewResponseModel>>
                {
                    Message = "success",
                    Total = total,
                    Data = products
                };
            }

            return null;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        
        
    }
}