

namespace Shop.Domain.Options
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
        
        public string? RefreshKey { get; set; }

        public const string IssUser = "tetetttettett";
        public const string Audienc = "hhdhdhdhdh";
    }
}
