using MediatR;
using Microsoft.Extensions.Options;
using Shop.Domain.Entities;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Shop.Domain.Options;
using Shop.Infratructure.Services.RedisService;
using Shop.Domain.Ultils;

namespace Shop.Aplication.Commands
{
    public class LoginAccountCommand:IRequest<LoginResponseModel?>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }

        public LoginAccountCommand(string? email, string? password)
        {
            Email = email;
            Password = password;
        }
        public LoginAccountCommand()
        {
            
        }
    }

    public class HandLoginUserCommand : IRequestHandler<LoginAccountCommand, LoginResponseModel?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<JwtOptions> _optionsMonitor;
        private readonly IRedisService _redis;
        public HandLoginUserCommand(IUnitOfWork unitOfWork, IOptions<JwtOptions> optionsMonitor, IRedisService redis)
        {
            _unitOfWork = unitOfWork;
            _optionsMonitor = optionsMonitor;
            _redis = redis;
        }

        public async Task<LoginResponseModel?> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
        {
            var user = await  _unitOfWork.userRepository.GetUserByEmailAndRole(request.Email);
            // find user by Email

            if (user == null || user.Password != request.Password|| user.IsActive is false) return null;
            //check user is null or password not equals return Unauthorization;

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.PrimarySid,user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.Role!.Name!.ToString()!)
            };
            var accessToken =
               JwtUltil. GenerateToken(_optionsMonitor.Value.IssuerSigningKey!,claims, DateTime.UtcNow.AddMinutes(1));

            var refreshToken = 
                JwtUltil.GenerateToken(_optionsMonitor.Value.RefreshKey!, claims, DateTime.UtcNow.AddDays(7));


            //Generate Access-token and RefreshToken


            await _redis.SetCacheAsync(user.Id.ToString(), refreshToken, DateTime.UtcNow.AddDays(7), cancellationToken);
            //save refreshToken to redis
            return  new LoginResponseModel(accessToken, refreshToken);
            //return AccessToken and RefreshToken for User

        }



       
    }
}
