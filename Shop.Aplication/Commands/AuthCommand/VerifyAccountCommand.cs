using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands;

public class VerifyAccountCommand:IRequest<bool>
{
    public string? AccountToken { get; set; }
}

public class HandVerifyAccount : IRequestHandler<VerifyAccountCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public HandVerifyAccount(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<bool> Handle(VerifyAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.AccountToken != null)
            {
                var validationToken = GetClaimsPrincipal(request.AccountToken);
                
                var email = validationToken.FindFirstValue(ClaimTypes.Email);

                if (email is null) return false;

                var user = await _unitOfWork.userRepository.GetUserByEmailAndRole(email);
                
                if (user == null) return false;
                
                user.IsActive = true;
                
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