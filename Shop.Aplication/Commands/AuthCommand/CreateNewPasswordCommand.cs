using MediatR;
using Shop.Aplication.ResultOrError;
using Shop.Infratructure.Services.RedisService;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands;

public class CreateNewPasswordCommand:IRequest<IResult<Object>>
{
    public string? Email { get; set; } 
    public int Number { get; set; }
    public string? Passsword { get; set; }
}

public class HandCreateNewPasswordCommand : IRequestHandler<CreateNewPasswordCommand, IResult<Object>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRedisService _redisService;

    public HandCreateNewPasswordCommand(IUnitOfWork unitOfWork, IRedisService redisService)
    {
        _unitOfWork = unitOfWork;
        _redisService = redisService;
    }

    public async Task<IResult<Object>> Handle(CreateNewPasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var getCode = await _redisService.GetCacheAsync($@"ResetPassword-{request.Email}", cancellationToken);
        
            if (string.IsNullOrEmpty(getCode)) return new BadRequest<object>("Code expire");

            if (getCode.Equals(request.Number.ToString()))
            {
                var user = await _unitOfWork.userRepository.GetUserByEmail(request.Email);

                if (user != null) user.Password = request.Passsword;

                await _unitOfWork.SaveChangesAsync();
            
                return new Ok<object>("Success", new { });
            }

            return new BadRequest<object>("The code is not the same");
        }
        catch (Exception e)
        {
            return new ServerError<object>(e.Message);
        }
        
    }
}