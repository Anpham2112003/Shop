using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Entities;
using Shop.Domain.Ultils;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Queries;

public class GetMyAddress:IRequest<List<Address>>
{
  
}

public class HandGetAddressByUserId:IRequestHandler<GetMyAddress,List<Address>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;

    public HandGetAddressByUserId(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    public async Task<List<Address>> Handle(GetMyAddress request, CancellationToken cancellationToken)
    {
        try
        {
            var UserId = Guid.Parse(_contextAccessor.HttpContext!.User.GetIdFromClaim()); 

            var address = await _unitOfWork.addressRepository.GetAddressByUserId(UserId);
 
            return address;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }
}