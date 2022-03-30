using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NLog;

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
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetAllUsersQueryHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IEnumerable<BaseUserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await Task.FromResult(userManager.Users.Select(x => new BaseUserDto
            {
                Id = x.Id,
                Username = x.UserName
            }).ToList());

            logger.Info("{amount} of {nameof} have been returned", result.Count, nameof(BaseUserDto));

            return result;
        }
    }
}