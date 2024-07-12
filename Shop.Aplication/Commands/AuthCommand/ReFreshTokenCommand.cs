using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shop.Domain.Entities;
using Shop.Domain.Options;
using Shop.Domain.ResponseModel;
using Shop.Domain.Ultils;
using Shop.Infratructure.Services.RedisService;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands;

public class ReFreshTokenCommand:IRequest<LoginResponseModel?>
{
    public string? Token { get; set; }

    public ReFreshTokenCommand(string token)
    {
        Token = token;
    }

    public ReFreshTokenCommand()
    {
        
    }
}

public class HandReFreshTokenCommand : IRequestHandler<ReFreshTokenCommand, LoginResponseModel?>
{
    private readonly IOptions<JwtOptions> _options;
    private readonly IRedisService _redis;
    private readonly IUnitOfWork _unitOfWork;
    public HandReFreshTokenCommand(IOptions<JwtOptions> options, IRedisService redis, IUnitOfWork unitOfWork)
    {
        _options = options;
        _redis = redis;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginResponseModel?> Handle(ReFreshTokenCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validateResult =  JwtUltil.ValidateToken(_options.Value.RefreshKey!,request.Token!,out var claims);

            //validate token if token not valid return unAuthorization 
            
            if (validateResult)
            {
                var email = claims!.GetEmailFromClaim();

                var user = await _unitOfWork.userRepository.GetUserByEmailAndRole(email);

                if (user!.IsActive is false) return null;

                var newclaims = new Claim[]
                {
                    new Claim(ClaimTypes.PrimarySid,user!.Id.ToString()),
                    new Claim(ClaimTypes.Email,user.Email!),
                    new Claim(ClaimTypes.Role,user.Role!.Name!.ToString())
                };
              
                var accessToken = JwtUltil. GenerateToken(_options.Value.IssuerSigningKey!,newclaims, DateTime.UtcNow.AddMinutes(1));
                
                var refreshToken= JwtUltil. GenerateToken(_options.Value.RefreshKey!,newclaims,DateTime.UtcNow.AddDays(7));

                return  new LoginResponseModel(accessToken, refreshToken);

            }

            return null;
        }
        catch (Exception )
        {
            throw;
        }
    }

    

   
}