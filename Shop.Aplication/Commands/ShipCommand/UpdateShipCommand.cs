using MediatR;
using Shop.Domain.Enums;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.ShipCommnd;

public class UpdateShipCommand:IRequest<bool>
{
    public Guid Id { get; set; }
    
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
            var ship = await _unitOfWork.shipRepository.FindByIdAsync(request.Id);
            
            if (ship is null||ship.State == ShipState.Shipping) return false;

            ship.State = request.ShipState;

            await _unitOfWork.SaveChangesAsync();
            
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }
}