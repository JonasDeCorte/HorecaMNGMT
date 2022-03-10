using Horeca.Core.Exceptions;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Horeca.Core.Handlers.Commands.Roles
{
    public class EditRoleCommand : IRequest<string>
    {
        public MutateRoleDto Model { get; }

        public EditRoleCommand(MutateRoleDto model)
        {
            Model = model;
        }
    }

    public class EditRoleCommandHandler : IRequestHandler<EditRoleCommand, string>
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public EditRoleCommandHandler(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<string> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var identityRole = await roleManager.FindByIdAsync(request.Model.Id);

            if (identityRole is null)
            {
                throw new EntityNotFoundException("role does not exist");
            }
            identityRole.Name = request.Model.RoleName ?? identityRole.Name;
            await roleManager.UpdateAsync(identityRole);

            return identityRole.Id;
        }
    }
}