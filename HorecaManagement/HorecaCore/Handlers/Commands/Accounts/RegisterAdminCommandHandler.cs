using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NLog;

namespace Horeca.Core.Handlers.Commands.Accounts;

public class RegisterAdminCommand : IRequest<string>
{
    public RegisterAdminCommand(RegisterUserDto model)
    {
        Model = model;
    }

    public RegisterUserDto Model { get; }
}

public class RegisterAdminCommandHandler : IRequestHandler<RegisterAdminCommand, string>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IUnitOfWork repository;

    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    public RegisterAdminCommandHandler(UserManager<ApplicationUser> userManager, IUnitOfWork repository)
    {
        this.userManager = userManager;
        this.repository = repository;
    }

    public async Task<string> Handle(RegisterAdminCommand request, CancellationToken cancellationToken)
    {
        logger.Info("trying to register {object} with username: {username}", nameof(ApplicationUser), request.Model.Username);

        var userExists = await userManager.FindByNameAsync(request.Model.Username);
        if (userExists != null)
        {
            logger.Error(RegisterException.Instance);
            throw new RegisterException();
        }

        ApplicationUser user = new()
        {
            Email = request.Model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Model.Username,
            ExternalId = Guid.NewGuid().ToString(),
            IsEnabled = true,
        };
        var result = await userManager.CreateAsync(user, request.Model.Password);
        logger.Info("added new admin {user}", user.NormalizedUserName);

        if (!result.Succeeded)
        {
            logger.Error(RegisterException.Instance);
            throw new RegisterException();
        }

        var listPermissions = repository.PermissionRepository.GetAll();

        foreach (var permission in listPermissions)
        {
            logger.Info("adding all permissions in total: {permCount} to {user}", listPermissions.Count(), user.NormalizedUserName);

            var userPerm = new UserPermission
            {
                PermissionId = permission.Id,
                UserId = user.Id
            };
            repository.UserPermissions.Add(userPerm);
        }
        await repository.CommitAsync();
        logger.Info("added: {permCount} to {user}", user.Permissions.Count, user.NormalizedUserName);
        return user.Id;
    }
}