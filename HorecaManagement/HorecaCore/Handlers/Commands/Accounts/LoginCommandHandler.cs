using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Horeca.Core.Handlers.Commands.Accounts
{
    public class LoginCommand : IRequest<object>
    {
        public LoginCommand(LoginUserDto model)
        {
            Model = model;
        }

        public LoginUserDto Model { get; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, object>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }

        public async Task<object> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.Model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, request.Model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = AccountTokens.GetToken(authClaims, configuration);

                return new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                };
            }
            return null;
        }
    }
}