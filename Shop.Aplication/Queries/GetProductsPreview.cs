using MediatR;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Queries;

public class GetProductsPreview : IRequest<PagingResponseModel<ProductPreviewResponseModel>?>
{
    public int Page { get; set; }
    public int Take { get; set; }

    public GetProductsPreview(int page, int take)
    {
        Page = page;
        Take = take;
    }
}

public class HandGetAllProductPreview : IRequestHandler<GetProductsPreview, PagingResponseModel<ProductPreviewResponseModel>?>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandGetAllProductPreview(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagingResponseModel<ProductPreviewResponseModel>?> Handle(GetProductsPreview request, CancellationToken cancellationToken)
    {
        try
        {
            var products =
                await _unitOfWork.productRepository
                    .GetProductPreview(request.Page, request.Take);

            var total = await _unitOfWork.productRepository.Count();

            if (products.Any())
            {
                return new PagingResponseModel<ProductPreviewResponseModel>
                {
                    
                    TotalPage = total / request.Take,
                    Data = products,
                    CurrentPage = request.Page,
                };
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }


    }
}