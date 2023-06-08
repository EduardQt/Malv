using System.Security.Claims;

namespace Malv.Services.Auth
{
    public class JwtClaims
    {
        public const string Id = "http://afterparty.com/identity/claims/id";
        public const string Name = ClaimTypes.Name;
        public const string Email = ClaimTypes.Email;
        public const string NameIdentifier = ClaimTypes.NameIdentifier;
        public const string Expiration = ClaimTypes.Expiration;
    }
}