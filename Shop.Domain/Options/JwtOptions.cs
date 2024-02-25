using Microsoft.IdentityModel.Tokens;

namespace Shop.Api.Auth
{
    public class JwtOptions
    {
        public const string Jwt = "Jwt";
        public bool ValidateIssuer {  get; set; }
        public bool ValidateActor { get; set; }
        public bool Audience {  get; set; }
        public bool ValidateLifetime { get; set; }
        public string? IssuerSigningKey {  get; set; }
        public string? ValidAudience { get; set; }
        public string? ValidIssuer { get; set; }

    }
}
