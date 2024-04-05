using MediatR;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.AddressCommand;

public class DeleteAddressCommand:IRequest<bool>
{
    public Guid Id { get; set; }

    public DeleteAddressCommand(Guid id)
    {
        Id = id;
    }
}

public class HandDeleteAddressCommand:IRequestHandler<DeleteAddressCommand,bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandDeleteAddressCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var address = await _unitOfWork.addressRepository.FindByIdAsync(request.Id);

            if (address is null) return false;
        
            _unitOfWork.addressRepository.Remove(address);

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