using MediatR;
using Shop.Aplication.ResultOrError;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands;

public class ChangePasswordCommand:IRequest<IResult<Object>>
{
    public string? Email { get; set; }
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
    
}

public class HandChangePasswordCommand : IRequestHandler<ChangePasswordCommand, IResult<Object>>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandChangePasswordCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult<object>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _unitOfWork.userRepository.GetUserByEmail(request.Email);
        
            if (user is null || user.Password.Equals(request.OldPassword)) return new NotFound<object>("User not exits");

            user.Password = request.NewPassword;
        
            await _unitOfWork.SaveChangesAsync();

            return new Ok<object>("success", new { });
        }
        catch (Exception e)
        {
            return new ServerError<object>(e.Message);
        }
        

    }
}