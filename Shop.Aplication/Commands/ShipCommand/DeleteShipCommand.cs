using MediatR;
using Shop.Domain.Enums;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.ShipCommnd;

public class DeleteShipCommand:IRequest<bool>
{
    public Guid Id { get; set; }
    
}

public class HandDeleteShip:IRequestHandler<DeleteShipCommand,bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandDeleteShip(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteShipCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var ship = await _unitOfWork.shipRepository.FindByIdAsync(request.Id);
            
            if (ship is null || ship.State==ShipState.Success) return false;
            
            var order = await _unitOfWork.orderRepository.GetByIdAsync(ship.OrderId);

            if (order is null) return false;

             order.OrderState=OrderState.Faild;

            _unitOfWork.shipRepository.Remove(ship);

            _unitOfWork.orderRepository.Update(order);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        catch (Exception )
        {
            
            throw;
        }
        
    }
}