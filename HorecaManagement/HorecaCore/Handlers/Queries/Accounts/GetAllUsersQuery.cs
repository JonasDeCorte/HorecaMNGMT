using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Horeca.Core.Handlers.Queries.Accounts
{
    public class GetAllUsersQuery : IRequest<IEnumerable<BaseUserDto>>
    {
        public GetAllUsersQuery()
        {
        }
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<BaseUserDto>>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public GetAllUsersQueryHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IEnumerable<BaseUserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(userManager.Users.Select(x => new BaseUserDto
            {
                Username = x.UserName
            }).ToList());
        }
    }
}