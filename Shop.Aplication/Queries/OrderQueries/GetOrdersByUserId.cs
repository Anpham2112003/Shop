using MediatR;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.UnitOfWork;
using StackExchange.Redis;

namespace Shop.Aplication.Queries.OrderQueries;

public class GetOrdersSuccess:IRequest<PagingResponseModel<List<OrderSuccessResponseModel>>?>
{
    public Guid UserId { get; set; }
    
    public int Page { get; set; }
    
    public int Take { get; set; }

    public GetOrdersSuccess(Guid userId, int page, int take)
    {
        UserId = userId;
        Page = page;
        Take = take;
    }
}

public class HandGetOrderSuccess:IRequestHandler<GetOrdersSuccess,PagingResponseModel<List<OrderSuccessResponseModel>>?>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandGetOrderSuccess(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagingResponseModel<List<OrderSuccessResponseModel>>?> Handle(GetOrdersSuccess request, CancellationToken cancellationToken)
    {
        var result =
            await _unitOfWork.orderRepository.GetOrderSuccessUserId(request.UserId, request.Page, request.Take);

        var total = await _unitOfWork.orderRepository.CountOrderSuccessByIdUser(request.UserId);
        
        
        if (result.Any())
        {
            return new PagingResponseModel<List<OrderSuccessResponseModel>>()
            {
                Message = "Success",
                Data = result,
                Total = total
            };
        }

        return null;
    }
}