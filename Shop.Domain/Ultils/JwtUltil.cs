
using Microsoft.IdentityModel.Tokens;
using Shop.Domain.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Ultils
{
    public static class JwtUltil
    {
        public static string GenerateToken(string key , IEnumerable<Claim> claims, DateTime expire)
        {
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = JwtOptions.IssUser,
                Audience = JwtOptions.Audienc,
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256),
                Expires = expire

            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            return jwtToken;
        }

        public static  bool ValidateToken(string key , string token,out ClaimsPrincipal? claims)
        {
           
            try
            {
                TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidateLifetime = true,
                    ValidateActor = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience=JwtOptions.Audienc,
                    ValidIssuer=JwtOptions.IssUser
                };

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                var result = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

                claims=result;

                return true;
            }
            catch (Exception)
            {

                claims = null;

                return false;
            }
          
        }
    }
}
