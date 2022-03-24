using Horeca.Core.Helpers;
using Horeca.Shared.AuthUtils;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Data.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Horeca.Core.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly JwtSettings jwtSettings;

        public AccessTokenService(ITokenGenerator tokenGenerator, JwtSettings jwtSettings) =>
            (this.tokenGenerator, this.jwtSettings) = (tokenGenerator, jwtSettings);

        public string Generate(ApplicationUser user)
        {
            List<Claim> claims = new()
            {
                new Claim("id", user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(StandardJwtClaimTypes.Subject, user.ExternalId)
            };
            return tokenGenerator.Generate(jwtSettings.AccessTokenSecret, jwtSettings.Issuer, jwtSettings.Audience,
                jwtSettings.AccessTokenExpirationMinutes, claims);
        }
    }
}