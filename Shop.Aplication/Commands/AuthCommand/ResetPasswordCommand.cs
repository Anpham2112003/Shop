using System.Security.Cryptography;
using MediatR;
using Shop.Infratructure.Services;
using Shop.Infratructure.Services.RedisService;
using Shop.Infratructure.UnitOfWork;
using StackExchange.Redis;

namespace Shop.Aplication.Commands;

public class ResetPasswordCommand:IRequest<Object?>
{
    public string? Email { get; set; }
}

public class HandResetPassword : IRequestHandler<ResetPasswordCommand, Object?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRedisService _redisService;
    private readonly IMailerSeverive _mailerService;

    public HandResetPassword(IUnitOfWork unitOfWork, IRedisService redisService, IMailerSeverive mailerService)
    {
        _unitOfWork = unitOfWork;
        _redisService = redisService;
        _mailerService = mailerService;
    }

    public async Task<object?> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var findEmail = await _unitOfWork.userRepository.GetUserByEmailAndRole(request.Email);

            if (findEmail is null) return null;
        
            var random = new Random();
        
            var number=  random.Next(1000, 9999);
        
            await  _redisService.SetCacheAsync($@"ResetPassword-{findEmail.Email}", number.ToString(),
                DateTime.UtcNow.AddMinutes(2), cancellationToken);

            await _mailerService.SendMail(findEmail.Email, "Reset password", EmailTemplate.ResetPassword(number.ToString()));
            

            return new
            {
                message = "success",
                email = findEmail.Email
            };
        }
        catch (Exception e)
        {
            return new Exception(e.Message);
        }
       
    }
}