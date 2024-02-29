using AutoMapper;
using MediatR;
using Shop.Aplication.ResultOrError;
using Shop.Domain.Entities;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands;

public class UpdateUserCommand:IRequest<IResult<UpdateUserCommand>>
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

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, IResult<UpdateUserCommand>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IResult<UpdateUserCommand>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var finduser = await _unitOfWork.userRepository.FindByIdAsync(request.Id);
        // find user by id
        if (finduser is null) return new NotFound<UpdateUserCommand>("Not found User by Id = " + request.Id);
        // check if user is ull return NotFound

        try
        {
           var user= _mapper.Map<UpdateUserCommand,User>(request,finduser);
           // map data UpdateUserCommand to variable finduser;
           _unitOfWork.userRepository.Update(user);
          await _unitOfWork.SaveChangesAsync();// commit data to database
          return new Ok<UpdateUserCommand>("Success", request); 
        }
        catch (Exception ex )
        {
            return new ServerError<UpdateUserCommand>("Cant update user because server error  "+ex.Message);
        }
    }
}