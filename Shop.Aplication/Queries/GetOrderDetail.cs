using MediatR;
using Shop.Domain.Entities;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Queries;

public class GetOrderDetail : IRequest<OrderDetailResponseModel?>
{
    public Guid Id { get; set; }

    public GetOrderDetail(Guid id)
    {
        Id = id;
    }
}

public class HandGetOrderDetail : IRequestHandler<GetOrderDetail, OrderDetailResponseModel?>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandGetOrderDetail(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderDetailResponseModel?> Handle(GetOrderDetail request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _unitOfWork.orderRepository.GetOrderDetail(request.Id);

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}