using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Shop.Domain.Entities;
using Shop.Domain.Ultils;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands;

public class UpdateUserCommand:IRequest<bool>
{
    public string? FistName { get; set; }
    public string? LastName {  get; set; }
    public string? FullName { get; set; }
    public DateTime UpdatedAt => DateTime.UtcNow;

    
    
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;
    public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {

        try
        {
            var UserId = Guid.Parse(_contextAccessor.HttpContext!.User.GetIdFromClaim());

            var User = await _unitOfWork.userRepository.FindByIdAsync(UserId);
            // find user by id
            if (User is null) return false;
            // check if user is ull return NotFound
           var user= _mapper.Map<UpdateUserCommand,User>(request,User);
           // map data UpdateUserCommand to variable findUser;
           _unitOfWork.userRepository.Update(user);

          await _unitOfWork.SaveChangesAsync();// commit data to database

          return true;
        }
        catch (Exception  )
        {
            throw ;
        }
    }
}