using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Domain.ResponseModel;
using Shop.Domain.Ultils;
using Shop.Infratructure.UnitOfWork;
using StackExchange.Redis;

namespace Shop.Aplication.Queries;

public class GetOrders : IRequest<PagingResponseModel<OrderResponseModel>?>
{

    public int Page { get; set; }

    public int Take { get; set; }

    public GetOrders( int page, int take)
    {
       
        Page = page;
        Take = take;
    }
}

public class HandGetOrderByUserId : IRequestHandler<GetOrders, PagingResponseModel<OrderResponseModel>?>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IHttpContextAccessor _contextAccessor;

    public HandGetOrderByUserId(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    public async Task<PagingResponseModel<OrderResponseModel>?> Handle(GetOrders request, CancellationToken cancellationToken)
    {
        var UserId = Guid.Parse(_contextAccessor.HttpContext!.User.GetIdFromClaim());

        var result =
            await _unitOfWork.orderRepository.GetOrderByUserId(UserId, request.Page, request.Take);

        var total = await _unitOfWork.orderRepository.CountOrderIdUser(UserId);


        if (result.Any())
        {
            return new PagingResponseModel<OrderResponseModel>()
            {
                
                Data = result,
                TotalPage = total / request.Take,
                CurrentPage = request.Page,
            };
        }

        return null;
    }
}