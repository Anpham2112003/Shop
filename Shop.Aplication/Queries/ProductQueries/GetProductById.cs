using MediatR;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Queries.ProductQueries;

public class GetProductById:IRequest<ProductResponseModel?>
{
    public Guid Id { get; set; }
}

public class HandGetProductById:IRequestHandler<GetProductById,ProductResponseModel?>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandGetProductById(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductResponseModel?> Handle(GetProductById request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _unitOfWork.productRepository.GetProductDetails(request.Id);
            
            
            if (product is null) return null;
            

            return product;


        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}