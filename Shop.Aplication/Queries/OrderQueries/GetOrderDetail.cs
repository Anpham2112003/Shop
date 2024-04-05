using MediatR;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Queries.OrderQueries;

public class GetOrderDetail:IRequest<Order?>
{
    public Guid Id { get; set; }

    public GetOrderDetail(Guid id)
    {
        Id = id;
    }
}

public class HandGetOrderDetail:IRequestHandler<GetOrderDetail,Order?>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandGetOrderDetail(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Order?> Handle(GetOrderDetail request, CancellationToken cancellationToken)
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