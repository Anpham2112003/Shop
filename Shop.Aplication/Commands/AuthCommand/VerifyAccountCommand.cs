using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop.Domain.Ultils;
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
           
            var validationToken = JwtUltil.ValidateToken(_configuration["EmailConfirm:VerifyUser"]!,request.AccountToken!,out var claims);

            if (validationToken)
            {
                var email = claims!.GetEmailFromClaim();

                var user = await _unitOfWork.userRepository.GetUserByEmailAndRole(email);

                user!.IsActive = true;

                await _unitOfWork.SaveChangesAsync();

                return true;
            }
          
            return false;
        }
        catch (Exception )
        {
            throw;
        }
    }

   
}