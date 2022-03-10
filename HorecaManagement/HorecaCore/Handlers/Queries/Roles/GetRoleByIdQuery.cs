using Horeca.Core.Exceptions;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Identity;

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

        public GetRoleByIdQueryHandler(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await Task.FromResult(roleManager.Roles.Select(x => new RoleDto
            {
                Id = x.Id,
                RoleName = x.Name
            }).Where(x => x.Id.Equals(request.RoleId)).FirstOrDefault());

            if (role is null)
            {
                throw new EntityNotFoundException("role does not exist");
            }
            return role;
        }
    }
}