using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Horeca.Core.Handlers.Commands.Accounts
{
    public class RegisterCommand : IRequest<int>
    {
        public RegisterCommand(RegisterUserDto model)
        {
            Model = model;
        }

        public RegisterUserDto Model { get; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, int>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }

        public async Task<int> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var userExists = await userManager.FindByNameAsync(request.Model.Username);
            if (userExists != null)
                throw new ArgumentException("User already exist");

            ApplicationUser user = new()
            {
                Email = request.Model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Model.Username
            };
            var result = await userManager.CreateAsync(user, request.Model.Password);
            if (!result.Succeeded)
                throw new ArgumentNullException("Creating user failed");

            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(new IdentityRole("User"));

            if (await roleManager.RoleExistsAsync("User"))
            {
                await userManager.AddToRoleAsync(user, "User");
            }
            return user.Id;
        }
    }
}