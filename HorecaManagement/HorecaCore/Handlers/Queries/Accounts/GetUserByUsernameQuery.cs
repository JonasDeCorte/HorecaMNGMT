using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Horeca.Core.Handlers.Queries.Accounts
{
    public class GetUserByUsernameQuery : IRequest<UserDto>
    {
        public GetUserByUsernameQuery(string username)
        {
            Username = username;
        }

        public string Username { get; }
    }

    public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, UserDto>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public GetUserByUsernameQueryHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<UserDto> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            {
                var user = await userManager.FindByNameAsync(request.Username);
                var roles = await userManager.GetRolesAsync(user);

                return new UserDto
                {
                    Roles = roles.ToList(),
                    Username = user.UserName
                };
            }
        }
    }
}