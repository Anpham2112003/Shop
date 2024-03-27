using MediatR;
using Shop.Domain.Entities;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Queries.CartQueries;

public class GetCartByUserId:IRequest<PagingResponseModel<List<Cart>>>
{
    public Guid Id { get; set; }
    public int Page { get; set; }
    public int Take { get; set; }

    public GetCartByUserId(Guid id, int page, int take)
    {
        Id = id;
        Page = page;
        Take = take;
    }
}

public class HandGetCartByUserId:IRequestHandler<GetCartByUserId,PagingResponseModel<List<Cart>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandGetCartByUserId(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagingResponseModel<List<Cart>>> Handle(GetCartByUserId request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _unitOfWork.cartRepository.GetCartByUserId(request.Id, request.Page, request.Take);
            
            var totalItem = await _unitOfWork.cartRepository.CountCartByUserId(request.Id);

            return new PagingResponseModel<List<Cart>>()
            {
                Message = "success",
                Data = result,
                Total = totalItem
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);
        }
    }
}