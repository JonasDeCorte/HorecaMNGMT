using Horeca.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Horeca.Core.Handlers.Commands.Roles
{
    public class DeleteRoleCommand : IRequest<string>
    {
        public DeleteRoleCommand(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, string>
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public DeleteRoleCommandHandler(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<string> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var IdentityRole = await roleManager.FindByIdAsync(request.Id);
            if (IdentityRole == null)
            {
                throw new EntityNotFoundException("Role doesn't exist");
            }
            await roleManager.DeleteAsync(IdentityRole);

            return request.Id;
        }
    }
}