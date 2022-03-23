using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.Tokens;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeca.Core.Handlers.Commands.Accounts
{
    public class RefreshTokenCommand : IRequest<object>
    {
        public RefreshTokenCommand(TokenDto model)
        {
            Model = model;
        }

        public TokenDto Model { get; }
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, object>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public RefreshTokenCommandHandler(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<object> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            string? accessToken = request.Model.AccessToken;
            string? refreshToken = request.Model.RefreshToken;
            var principal = AccountTokens.GetPrincipalFromExpiredToken(accessToken, configuration);
            if (principal == null)
            {
                throw new ArgumentNullException("Invalid access token or refresh token");
            }
            string username = principal.Identity.Name;

            var user = await userManager.FindByNameAsync(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new ArgumentException("Invalid access token or refresh token");
            }

            var newAccessToken = AccountTokens.GetToken(principal.Claims.ToList(), configuration);
            var newRefreshToken = AccountTokens.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await userManager.UpdateAsync(user);

            return new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            };
        }
    }
}