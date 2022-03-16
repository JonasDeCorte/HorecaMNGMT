using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NLog;

namespace Horeca.Core.Handlers.Commands.Accounts;

public class RegisterAdminCommand : IRequest<int>
{
    public RegisterAdminCommand(RegisterUserDto model)
    {
        Model = model;
    }

    public RegisterUserDto Model { get; }
}

public class RegisterAdminCommandHandler : IRequestHandler<RegisterAdminCommand, int>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private static Logger logger = LogManager.GetCurrentClassLogger();

    public RegisterAdminCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    public async Task<int> Handle(RegisterAdminCommand request, CancellationToken cancellationToken)
    {
        logger.Info("trying to register {object} with username: {username}", nameof(ApplicationUser), request.Model.Username);

        var userExists = await userManager.FindByNameAsync(request.Model.Username);
        if (userExists != null)
        {
            logger.Error("creating user failed, user already exists");
            throw new ArgumentException("User already exist");
        }

        ApplicationUser user = new()
        {
            Email = request.Model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Model.Username
        };
        var result = await userManager.CreateAsync(user, request.Model.Password);
        logger.Info("added new admin {user}", user.NormalizedUserName);

        if (!result.Succeeded)
        {
            logger.Error("creating user failed");

            throw new ArgumentNullException($"Creating {nameof(user)} failed");
        }

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            logger.Info("added role admin");
        }

        if (await roleManager.RoleExistsAsync("Admin"))
        {
            await userManager.AddToRoleAsync(user, "Admin");
            logger.Info("added role admin to new admin {user}", user.NormalizedUserName);
        }

        return user.Id;
    }
}