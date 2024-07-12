using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Ultils;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.AddressCommand;

public class UpdateAddressCommand:IRequest<bool>
{
    public Guid Id { get; set; }
    public string? StreetAddress { get; set; }
    public string? Commune { get; set; }
    public string? District { get; set; }
    public string? City { get; set; }
}

public class HandUpdateAddressCommand:IRequestHandler<UpdateAddressCommand,bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;
    public HandUpdateAddressCommand(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
    }

    public async Task<bool> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var address = await _unitOfWork.addressRepository.FindByIdAsync(request.Id);

            var userId = Guid.Parse(_contextAccessor.HttpContext!.User.GetIdFromClaim()); 

            if (address is null || address.UserId.Equals(userId) is false) return false;
            
            _mapper.Map(request, address);
        
            _unitOfWork.addressRepository.Update(address);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        catch (Exception )
        {
           
            throw;
        }
       
    }
}