using MediatR;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands;

public class ChangePasswordCommand:IRequest<bool>
{
    public string? Email { get; set; }
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
    
}

public class HandChangePasswordCommand : IRequestHandler<ChangePasswordCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public HandChangePasswordCommand(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _unitOfWork.userRepository.GetUserByEmailAndRole(request.Email);

            if (user is null || user.Password!=request.OldPassword) return false;

            user.Password = request.NewPassword;
        
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        

    }
}