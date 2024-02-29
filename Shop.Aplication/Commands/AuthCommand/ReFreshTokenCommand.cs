using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shop.Aplication.ResultOrError;
using Shop.Domain.Entities;
using Shop.Domain.Options;
using Shop.Domain.ResponseModel;
using Shop.Infratructure.Services.RedisService;
using Shop.Infratructure.UnitOfWork;

namespace Shop.Aplication.Commands;

public class ReFreshTokenCommand:IRequest<IResult<LoginResponseModel>>
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

public class HandReFreshTokenCommand : IRequestHandler<ReFreshTokenCommand, IResult<LoginResponseModel>>
{
    private readonly IOptions<JwtOptions> _options;
    private readonly IRedisService _redis;

    public HandReFreshTokenCommand(IOptions<JwtOptions> options, IUnitOfWork unitOfWork, IRedisService redis)
    {
        _options = options;
        _redis = redis;
    }

    public async Task<IResult<LoginResponseModel>> Handle(ReFreshTokenCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validateResult =  ValidateToken(request.Token);

            //validate token if token not valid return unAuthorization 
            
            if (validateResult.Claims.Any())
            {
                string? userId = validateResult.FindFirstValue("UserId");
                var token = await _redis.GetCacheAsync(userId,cancellationToken);
                // get token from redis if token is null return token expire
                if (string.IsNullOrEmpty(token)) return new UnAuthorization<LoginResponseModel>("Token expire");
                // check token 
               
                //decodeToken get information
               
                Debug.WriteLine(userId);
                //get UserId from claims ;
                var accessToken = await GenerateToken(validateResult.Claims,_options.Value.IssuerSigningKey, DateTime.UtcNow.AddMinutes(1));
                
                var refreshToken= await GenerateToken(validateResult.Claims,_options.Value.RefreshKey,DateTime.UtcNow.AddDays(7));

                await _redis.SetCacheAsync(userId, refreshToken, DateTime.UtcNow.AddDays(7), cancellationToken);
                //set cache to redis

                return new Ok<LoginResponseModel>("success", new LoginResponseModel(accessToken, refreshToken));

            }

            return new UnAuthorization<LoginResponseModel>("Token not valid");
        }
        catch (Exception e)
        {
            return new ServerError<LoginResponseModel>(e.Message);
        }
    }

    private ClaimsPrincipal ValidateToken(string? token)
    {

        TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.RefreshKey!)),
            ValidateLifetime = _options.Value.ValidateLifetime,
            ValidateActor = _options.Value.ValidateActor,
            ValidateIssuer = _options.Value.ValidateIssuer,
            ValidIssuer = _options.Value.ValidIssuer,
            ValidAudience = _options.Value.ValidAudience
        };
        
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        var result = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        return result;
    }

    private Task<string> GenerateToken(IEnumerable<Claim> claims,string key, DateTime time)
    {
        var keyByte = Encoding.UTF8.GetBytes(key);
        
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _options.Value.ValidIssuer,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(keyByte), SecurityAlgorithms.HmacSha256),
            Expires = time
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        return Task.FromResult(token);
    }

   
}