using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop.Aplication.ResultOrError;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands;

public class VerifyAccountCommand:IRequest<IResult<Object>>
{
    public string? AccountToken { get; set; }
}

public class HandVerifyAccount : IRequestHandler<VerifyAccountCommand, IResult<object>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public HandVerifyAccount(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<IResult<Object>> Handle(VerifyAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validationToken = GetClaimsPrincipal(request.AccountToken);
            var email = validationToken.FindFirstValue(ClaimTypes.Email);
            if (email is null)
            {
                return new UnAuthorization<Object>("Token not valid");
            }

            var user = await _unitOfWork.userRepository.GetUserByEmail(email);
            user.IsActive = true;
             await _unitOfWork.SaveChangesAsync();
            return new Ok<Object>("Success",new {});
        }
        catch (Exception e)
        {
            return new ServerError<object>(e.Message);
        }
    }

    private ClaimsPrincipal GetClaimsPrincipal(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        TokenValidationParameters parameters = new TokenValidationParameters()
        {
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["EmailConfirm:VerifyUser"])),
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = false
        };

        return tokenHandler.ValidateToken(token, parameters, out SecurityToken securityToken);
    }
}