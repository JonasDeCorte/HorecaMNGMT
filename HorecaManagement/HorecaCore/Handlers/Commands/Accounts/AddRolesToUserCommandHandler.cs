using Horeca.Core.Exceptions;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NLog;

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
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public AddRolesToUserCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<int> Handle(AddRolesToUserCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to create {object} with Id: {Id}", nameof(IdentityRole), request.Model.Username);

            var user = await userManager.FindByNameAsync(request.Model.Username);
            if (user == null)
            {
                logger.Error("user doesn't exist");
                throw new EntityNotFoundException("User doesn't exist");
            }

            foreach (var role in request.Model.Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                    logger.Info("creating new role with name: ", role);
                }

                var identityRole = await roleManager.FindByNameAsync(role);

                if (!await userManager.IsInRoleAsync(user, identityRole.Name))
                {
                    await userManager.AddToRoleAsync(user, identityRole.Name);
                    logger.Info("add role {name} to user {user}", identityRole.Name, user);
                }
            }

            return user.Id;
        }
    }
}