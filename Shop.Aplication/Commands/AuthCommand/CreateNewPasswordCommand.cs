using MediatR;
using Shop.Infratructure.Services.RedisService;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands;

public class CreateNewPasswordCommand:IRequest<bool>
{
    public string? Email { get; set; } 
    public int Number { get; set; }
    public string? Passsword { get; set; }

    public CreateNewPasswordCommand()
    {
        
    }
}

public class HandCreateNewPasswordCommand : IRequestHandler<CreateNewPasswordCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRedisService _redisService;

    public HandCreateNewPasswordCommand(IUnitOfWork unitOfWork, IRedisService redisService)
    {
        _unitOfWork = unitOfWork;
        _redisService = redisService;
    }

    public async Task<bool> Handle(CreateNewPasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var getCode = await _redisService.GetCacheAsync($@"ResetPassword-{request.Email}", cancellationToken);

            if (string.IsNullOrEmpty(getCode)) return false;

            if (getCode.Equals(request.Number.ToString()))
            {
                var user = await _unitOfWork.userRepository.GetUserByEmailAndRole(request.Email);

                if (user != null) user.Password = request.Passsword;

                await _unitOfWork.SaveChangesAsync();

                return true;
            }

            return false;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        
    }
}