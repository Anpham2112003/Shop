using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Entities;
using Shop.Domain.ResponseModel;
using Shop.Domain.Ultils;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Queries.CartQueries;

public class GetMyCart:IRequest<PagingResponseModel<CartResponseModel>>
{
  
    public int Page { get; set; }
    public int Take { get; set; }

    public GetMyCart( int page, int take)
    {
        
        Page = page;
        Take = take;
    }
}

public class HandGetCartByUserId:IRequestHandler<GetMyCart,PagingResponseModel<CartResponseModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public HandGetCartByUserId(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    public async Task<PagingResponseModel<CartResponseModel>> Handle(GetMyCart request, CancellationToken cancellationToken)
    {
        try
        {
            var UserId = Guid.Parse(_contextAccessor.HttpContext!.User.GetIdFromClaim());

            var result = await _unitOfWork.cartRepository.GetCartByUserId(UserId, request.Page, request.Take);
            
            var totalItem = await _unitOfWork.cartRepository.CountCartByUserId(UserId);

            return new PagingResponseModel<CartResponseModel>()
            {
                
                Data = result,
                TotalPage = totalItem / request.Take,
                CurrentPage=request.Page,
            };
        }
        catch (Exception )
        {
            
            throw;
        }
    }
}