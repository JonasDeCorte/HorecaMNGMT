using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Identity;

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
    private readonly IUnitOfWork repository;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public RegisterAdminCommandHandler(IUnitOfWork repository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        this.repository = repository;
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    public async Task<int> Handle(RegisterAdminCommand request, CancellationToken cancellationToken)
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

        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));

        if (await roleManager.RoleExistsAsync("Admin"))
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }

        return user.Id;
    }
}