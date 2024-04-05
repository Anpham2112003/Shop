using MediatR;
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

            if (ship is null) return false;
        
            _unitOfWork.shipRepository.Remove(ship);
            
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