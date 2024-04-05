using MediatR;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Queries;

public class GetAddressByUserId:IRequest<List<Address>>
{
    public Guid Id { get; set; }

    public GetAddressByUserId(Guid id)
    {
        Id = id;
    }
}

public class HandGetAddressByUserId:IRequestHandler<GetAddressByUserId,List<Address>>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandGetAddressByUserId(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Address>> Handle(GetAddressByUserId request, CancellationToken cancellationToken)
    {
        try
        {
            var address = await _unitOfWork.addressRepository.GetAddressByUserId(request.Id);
 

            return address;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }
}