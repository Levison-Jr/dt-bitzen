using Microsoft.IdentityModel.Tokens;

namespace DTBitzen.Identity
{
    public class JwtOptions
    {
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public int TokenExpiration { get; set; }
        public required SigningCredentials SigningCredentials { get; set; }
    }
}
