using Horeca.Core.Exceptions;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NLog;

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
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public EditRoleCommandHandler(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<string> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to edit {object} with Id: {Id}", request.Model, request.Model.Id);

            var identityRole = await roleManager.FindByIdAsync(request.Model.Id);

            if (identityRole is null)
            {
                logger.Error("{Object} with Id: {id} does not exist", nameof(identityRole), request.Model.Id);

                throw new EntityNotFoundException("role does not exist");
            }
            identityRole.Name = request.Model.RoleName ?? identityRole.Name;
            await roleManager.UpdateAsync(identityRole);
            logger.Info("updated {@object} with Id: {id}", identityRole, identityRole.Id);

            return identityRole.Id;
        }
    }
}