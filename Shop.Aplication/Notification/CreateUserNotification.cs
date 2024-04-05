using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop.Aplication.Commands;
using Shop.Infratructure.Services;


namespace Shop.Aplication.Notify;

public class CreateUserNotification:INotification
{
    public CreateUserNotification(CreateUserCommand userCommand)
    {
        UserCommand = userCommand;
    }

    public CreateUserCommand UserCommand { get; set; }
}

public class HandCreateUserNotify : INotificationHandler<CreateUserNotification>
{
  
    private readonly IMailerSeverive _mailerService;
    private readonly IConfiguration _configuration;

    public HandCreateUserNotify( IMailerSeverive mailerService, IConfiguration configuration)
    {
        _mailerService = mailerService;
        _configuration = configuration;
    }

    public async Task Handle(CreateUserNotification notification, CancellationToken cancellationToken)
    {
            var token = await GenerateToken(notification.UserCommand);

            await _mailerService.SendMail(notification.UserCommand.Email, "Xac minh tai khoan",
            EmailTemplate.VerifyEmail(token));

            await Task.CompletedTask;
    }
    

   

    private Task<string> GenerateToken(CreateUserCommand user)
    {
        var byteKey = Encoding.UTF8.GetBytes(_configuration["EmailConfirm:VerifyUser"]);
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("UserId", user.Id+"")
        };
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(byteKey), SecurityAlgorithms.HmacSha256),
            Expires = DateTime.UtcNow.AddMinutes(30)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var stringToken = tokenHandler.WriteToken(token);
        return Task.FromResult(stringToken);
    }
    

    
}