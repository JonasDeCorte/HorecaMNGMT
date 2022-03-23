using Horeca.Shared.AuthUtils;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Horeca.Core.Handlers.Commands.Accounts
{
    public class LoginCommand : IRequest<LoginResult>
    {
        public LoginCommand(LoginUserDto model)
        {
            Model = model;
        }

        public LoginUserDto Model { get; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.Model.Username);

            LoginResult result = null;

            if (user != null && await userManager.CheckPasswordAsync(user, request.Model.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(StandardJwtClaimTypes.Subject, user.ExternalId)
                };

                var token = AccountTokens.GetToken(authClaims, configuration);

                result = new LoginResult
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                };
            }

            return result;
        }
    }
}