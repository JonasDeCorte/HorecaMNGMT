using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NLog;

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
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public GetAllRolesQueryHandler(RoleManager<IdentityRole> roleManager)

        {
            this.roleManager = roleManager;
        }

        public async Task<IEnumerable<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var result = await Task.FromResult(roleManager.Roles.Select(x => new RoleDto
            {
                Id = x.Id,
                RoleName = x.Name
            }).ToList());

            logger.Info("{amount} of {nameof} have been returned", result.Count(), nameof(RoleDto));

            return result;
        }
    }
}