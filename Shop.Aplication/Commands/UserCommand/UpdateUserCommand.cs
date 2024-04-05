using AutoMapper;
using MediatR;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands;

public class UpdateUserCommand:IRequest<bool>
{
    public Guid Id { get; set; }
    public string? FistName { get; set; }
    public string? LastName {  get; set; }
    public string? FullName { get; set; }
    public string? Password { get; set; }
    public DateTime UpdatedAt => DateTime.UtcNow;

    public UpdateUserCommand(Guid id, string? fistName, string? lastName, string? fullName, string? password)
    {
        Id = id;
        FistName = fistName;
        LastName = lastName;
        FullName = fullName;
        Password = password;
    }
    
    
    
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {

        try
        {
            var findUser = await _unitOfWork.userRepository.FindByIdAsync(request.Id);
            // find user by id
            if (findUser is null) return false;
            // check if user is ull return NotFound
           var user= _mapper.Map<UpdateUserCommand,User>(request,findUser);
           // map data UpdateUserCommand to variable findUser;
           _unitOfWork.userRepository.Update(user);
          await _unitOfWork.SaveChangesAsync();// commit data to database
          return true;
        }
        catch (Exception ex )
        {
            throw new Exception(ex.Message);
        }
    }
}