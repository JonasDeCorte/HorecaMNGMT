using Horeca.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NLog;

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
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteRoleCommandHandler(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<string> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var identityRole = await roleManager.FindByIdAsync(request.Id);
            logger.Info("trying to delete {@object} with Id: {id}", identityRole, request.Id);

            if (identityRole == null)
            {
                logger.Error("{object} with Id: {id} is null", nameof(IdentityRole), request.Id);

                throw new EntityNotFoundException("Role doesn't exist");
            }
            await roleManager.DeleteAsync(identityRole);
            logger.Info("deleted {@object} with Id: {id}", identityRole, request.Id);

            return request.Id;
        }
    }
}