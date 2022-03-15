using Horeca.Core.Exceptions;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Horeca.Core.Handlers.Commands.Accounts
{
    public class AddRolesToUserCommand : IRequest<int>
    {
        public AddRolesToUserCommand(MutateRolesUserDto model)
        {
            Model = model;
        }

        public MutateRolesUserDto Model { get; }
    }

    public class AddRolesToUserCommandHandler : IRequestHandler<AddRolesToUserCommand, int>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AddRolesToUserCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<int> Handle(AddRolesToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.Model.Username);
            if (user == null)
                throw new EntityNotFoundException("User doesn't exist");

            foreach (var role in request.Model.Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole { Name = role });

                var identityRole = await roleManager.FindByNameAsync(role);

                if (!await userManager.IsInRoleAsync(user, identityRole.Name))
                    await userManager.AddToRoleAsync(user, identityRole.Name);
            }

            return user.Id;
        }
    }
}