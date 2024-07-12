using MediatR;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Queries;

public class GetProductDetail : IRequest<ProductResponseModel?>
{
    public Guid Id { get; set; }
}

public class HandGetProductById : IRequestHandler<GetProductDetail, ProductResponseModel?>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandGetProductById(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductResponseModel?> Handle(GetProductDetail request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _unitOfWork.productRepository.GetProductDetails(request.Id);


            if (product is null) return null;


            return product;


        }
        catch (Exception)
        {
            throw;
        }
    }
}