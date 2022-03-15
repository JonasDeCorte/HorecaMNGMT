using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Horeca.Core.Handlers.Queries.Roles

{
    public class GetAllRolesQuery : IRequest<IEnumerable<RoleDto>>
    {
        public GetAllRolesQuery()
        {
        }
    }

    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<RoleDto>>
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public GetAllRolesQueryHandler(RoleManager<IdentityRole> roleManager)

        {
            this.roleManager = roleManager;
        }

        public async Task<IEnumerable<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(roleManager.Roles.Select(x => new RoleDto
            {
                Id = x.Id,
                RoleName = x.Name
            }).ToList());
        }
    }
}