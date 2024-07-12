using MediatR;
using Shop.Domain.Enums;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.ShipCommnd;

public class UpdateShipCommand:IRequest<bool>
{
    public Guid ShipId { get; set; }
    public Guid OrderId {  get; set; }
    public ShipState ShipState { get; set; }
}

public class HandUpdateShipCommand:IRequestHandler<UpdateShipCommand,bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandUpdateShipCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateShipCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var ship = await _unitOfWork.shipRepository.FindByIdAsync(request.ShipId);

            var order = await _unitOfWork.orderRepository.FindByIdAsync(request.OrderId);

            if (ship is null || order is null || request.ShipState.Equals(ShipState.Waiting) ) return false;

            if(ship.State==ShipState.Shipping && request.ShipState==ShipState.Shipping) return false;


            if (request.ShipState==ShipState.Success)
            {
                
                order!.OrderState = OrderState.Success;
                 
            }
            if (request.ShipState==ShipState.Fail)
            {
                order.OrderState= OrderState.Faild;
            }

            await _unitOfWork.SaveChangesAsync();
            
            return true;
        }
        catch (Exception )
        {
            
            throw;
        }
       
    }
}