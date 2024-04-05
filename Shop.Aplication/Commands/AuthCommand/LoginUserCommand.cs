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

namespace Shop.Aplication.Commands
{
    public class LoginUserCommand:IRequest<LoginResponseModel?>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }

        public LoginUserCommand(string? email, string? password)
        {
            Email = email;
            Password = password;
        }
        public LoginUserCommand()
        {
            
        }
    }

    public class HandLoginUserCommand : IRequestHandler<LoginUserCommand, LoginResponseModel?>
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

        public async Task<LoginResponseModel?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await  _unitOfWork.userRepository.GetUserByEmailAndRole(request.Email);
            // find user by Email

            if (user == null || user.Password != request.Password) return null;
            //check user is null or password not equals return Unauthorization;

            var accessToken =await 
                GenerateToken(user, user.Role.Name, _optionsMonitor.Value.IssuerSigningKey, DateTime.UtcNow.AddMinutes(1));

            var refreshToken = await 
                GenerateToken(user, user.Role.Name, _optionsMonitor.Value.RefreshKey, DateTime.UtcNow.AddDays(7));
            
            
            //Generate Access-token and RefreshToken
            

            await _redis.SetCacheAsync(user.Id.ToString(), refreshToken, DateTime.UtcNow.AddDays(7), cancellationToken);
            //save refreshToken to redis
            return  new LoginResponseModel(accessToken, refreshToken);
            //return AccessToken and RefreshToken for User

        }



        private Task<string> GenerateToken(User user,string role,string key,DateTime offset)
        {
            var byteKey = Encoding.UTF8.GetBytes(key);
            
            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId",user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.Role,role )
                }
            );

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Issuer = _optionsMonitor.Value.ValidIssuer,
                Audience = _optionsMonitor.Value.ValidAudience,
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(byteKey), SecurityAlgorithms.HmacSha256),
                Expires = offset
                
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return Task.FromResult(jwtToken);


        }
    }
}
