using Horeca.Core.Exceptions;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NLog;

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
        private readonly IHttpContextAccessor httpContextAccessor;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public GetUserByUsernameQueryHandler(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserDto> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            logger.Info("trying to get {object} with username: {Id}", nameof(IdentityUser), request.Username);

            {
                var user = await userManager.FindByNameAsync(request.Username);
                if (user == null)
                {
                    logger.Error("user doesn't exist");
                    throw new EntityNotFoundException("User doesn't exist");
                }

                logger.Info("returning {name} with user {user}", nameof(UserDto), user.UserName);
                var perms = new List<Tuple<string, string>>();
                foreach (var item in httpContextAccessor.HttpContext.User.Claims)
                {
                    perms.Add(new Tuple<string, string>(item.Type, item.Value));
                }

                return new UserDto
                {
                    Permissions = perms,
                    Username = user.UserName
                };
            }
        }
    }
}