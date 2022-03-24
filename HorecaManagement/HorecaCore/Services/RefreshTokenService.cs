using Horeca.Core.Helpers;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Data.Services;

namespace Horeca.Core.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly JwtSettings jwtSettings;

        public RefreshTokenService(ITokenGenerator tokenGenerator, JwtSettings jwtSettings) =>
            (this.tokenGenerator, this.jwtSettings) = (tokenGenerator, jwtSettings);

        public string Generate(ApplicationUser user) => tokenGenerator.Generate(jwtSettings.RefreshTokenSecret,
            jwtSettings.Issuer, jwtSettings.Audience,
            jwtSettings.RefreshTokenExpirationMinutes);
    }
}