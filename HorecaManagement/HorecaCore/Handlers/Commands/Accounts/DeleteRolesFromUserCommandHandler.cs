using Horeca.Core.Exceptions;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Horeca.Core.Handlers.Commands.Accounts
{
    public class DeleteRolesFromUserCommand : IRequest<int>
    {
        public DeleteRolesFromUserCommand(MutateRolesUserDto model)
        {
            Model = model;
        }

        public MutateRolesUserDto Model { get; }
    }

    public class DeleteRolesFromUserCommandHandler : IRequestHandler<DeleteRolesFromUserCommand, int>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DeleteRolesFromUserCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<int> Handle(DeleteRolesFromUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.Model.Username);
            if (user == null)
                throw new EntityNotFoundException("User doesn't exist");

            foreach (var role in request.Model.Roles)
            {
                if (await roleManager.RoleExistsAsync(role))
                {
                    var identityRole = await roleManager.FindByNameAsync(role);

                    if (await userManager.IsInRoleAsync(user, identityRole.Name))
                        await userManager.RemoveFromRoleAsync(user, identityRole.Name);
                }
            }
            return user.Id;
        }
    }
}