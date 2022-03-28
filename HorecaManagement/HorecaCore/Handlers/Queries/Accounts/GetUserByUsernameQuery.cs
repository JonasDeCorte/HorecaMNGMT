using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NLog;
using System.Linq;

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
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public GetUserByUsernameQueryHandler(UserManager<ApplicationUser> userManager, IUnitOfWork repository)
        {
            this.userManager = userManager;
            this.repository = repository;
        }

        public async Task<UserDto> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            logger.Info("trying to get {object} with username: {Id}", nameof(ApplicationUser), request.Username);

            var user = await userManager.FindByNameAsync(request.Username);

            if (user == null)
            {
                logger.Error(UserNotFoundException.Instance);
                throw new UserNotFoundException();
            }

            logger.Info("returning {name} with user {user}", nameof(UserDto), user.UserName);

            var userPermissions = repository.UserPermissions.GetAllUserPermissionsByUserId(user.Id);

            List<PermissionDto>? permissions = (from userpermission in userPermissions
                                                let permission = repository.PermissionRepository.Get(userpermission.PermissionId)
                                                select new PermissionDto
                                                {
                                                    Id = permission.Id,
                                                    PermissionName = permission.Name,
                                                }).ToList();

            return new UserDto
            {
                Permissions = permissions,
                Username = user.UserName
            };
        }
    }
}