using AutoMapper;
using MediatR;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands.AddressCommand;

public class CreateAddressCommand:IRequest<bool>
{
    public Guid Id = Guid.NewGuid();
    public string? StreetAddress { get; set; }
    public string? Commune { get; set; }
    public string? District { get; set; }
    public string? City { get; set; }
    
    public Guid UserId { get; set; }

}

public class HandCreateAddressCommand:IRequestHandler<CreateAddressCommand,bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public HandCreateAddressCommand(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var address = _mapper.Map<Address>(request);
            
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