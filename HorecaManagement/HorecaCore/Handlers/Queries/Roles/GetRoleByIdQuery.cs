using Horeca.Core.Exceptions;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NLog;

namespace Horeca.Core.Handlers.Queries.Roles
{
    public class GetRoleByIdQuery : IRequest<RoleDto>
    {
        public GetRoleByIdQuery(string roleId)
        {
            RoleId = roleId;
        }

        public string RoleId { get; }
    }

    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto>
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public GetRoleByIdQueryHandler(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            logger.Info("trying to return {object} with id: {id}", nameof(RoleDto), request.RoleId);

            var role = await Task.FromResult(roleManager.Roles.Select(x => new RoleDto
            {
                Id = x.Id,
                RoleName = x.Name
            }).Where(x => x.Id.Equals(request.RoleId)).FirstOrDefault());

            if (role is null)
            {
                logger.Error("{object} with Id: {id} is null", nameof(role), request.RoleId);

                throw new EntityNotFoundException("role does not exist");
            }
            logger.Info("returning {@object} with id: {id}", role, request.RoleId);

            return role;
        }
    }
}