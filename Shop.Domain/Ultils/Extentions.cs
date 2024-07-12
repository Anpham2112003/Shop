using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Ultils
{
    public static class Extentions
    {
        public static string GetIdFromClaim(this ClaimsPrincipal claims)
        {
            var claim = claims.FindFirst(ClaimTypes.PrimarySid)!.Value;

            return claim;
        }

        public static string GetEmailFromClaim(this ClaimsPrincipal claims)
        {
            var claim = claims.FindFirst(ClaimTypes.Email)!.Value;

            return claim;
        }
    }
}
