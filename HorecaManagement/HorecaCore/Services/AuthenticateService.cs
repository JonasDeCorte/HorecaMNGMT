using Horeca.Shared;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Data.Services;

namespace Horeca.Core.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IAccessTokenService accessTokenService;
        private readonly IRefreshTokenService refreshTokenService;
        private readonly IApplicationDbContext context;

        public AuthenticateService(IAccessTokenService accessTokenService, IRefreshTokenService refreshTokenService, IApplicationDbContext context)
        {
            this.accessTokenService = accessTokenService;
            this.refreshTokenService = refreshTokenService;
            this.context = context;
        }

        public async Task<TokenResultDto> Authenticate(ApplicationUser user, CancellationToken cancellationToken)
        {
            var refreshToken = refreshTokenService.Generate(user);
            await context.RefreshTokens.AddAsync(new RefreshToken(user.Id, refreshToken), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            var accessToken = accessTokenService.Generate(user);

            return new TokenResultDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }
    }
}