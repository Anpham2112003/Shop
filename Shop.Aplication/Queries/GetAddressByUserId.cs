using MediatR;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Queries.AddressQueies;

public class GetAddressByUserId:IRequest<Address?>
{
    public Guid Id { get; set; }

    public GetAddressByUserId(Guid id)
    {
        Id = id;
    }
}

public class HandGetAddressByUserId:IRequestHandler<GetAddressByUserId,Address?>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandGetAddressByUserId(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Address?> Handle(GetAddressByUserId request, CancellationToken cancellationToken)
    {
        try
        {
            var address = await _unitOfWork.addressRepository.GetAddressByUserId(request.Id);

            if (address is null) return null;

            return address;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }
}