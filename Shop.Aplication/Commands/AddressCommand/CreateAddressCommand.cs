using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Entities;
using Shop.Domain.Ultils;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.AddressCommand;

public class CreateAddressCommand:IRequest<bool>
{
    public Guid Id = Guid.NewGuid();
    public string? StreetAddress { get; set; }
    public string? Commune { get; set; }
    public string? District { get; set; }
    public string? City { get; set; }
    

}

public class HandCreateAddressCommand:IRequestHandler<CreateAddressCommand,bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;


    public HandCreateAddressCommand(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
    }

    public async Task<bool> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var address = _mapper.Map<Address>(request);

            address.UserId = Guid.Parse(_contextAccessor.HttpContext!.User.GetIdFromClaim());

            await _unitOfWork.addressRepository.AddAsync(address);
            
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