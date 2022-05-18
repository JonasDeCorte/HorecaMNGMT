using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Dtos.Accounts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NLog;

namespace Horeca.Core.Handlers.Commands.Accounts
{
    public class RegisterCommand : IRequest<string>
    {
        public RegisterCommand(RegisterUserDto model)
        {
            Model = model;
        }

        public RegisterUserDto Model { get; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IUnitOfWork repository)
        {
            this.userManager = userManager;
            this.repository = repository;
        }

        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
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
                IsOwner = request.Model.IsOwner,
            };

            var result = await userManager.CreateAsync(user, request.Model.Password);
            if (result.Succeeded)
            {
                logger.Info("added new user {user}", user.NormalizedUserName);

                AddNewUserPermission(user);
                AddDefaultUserPermissions(user);
                IsOwnerPermissions(user);

                await repository.CommitAsync();
            }

            if (!result.Succeeded)
            {
                logger.Error(RegisterException.Instance);
                throw new RegisterException();
            }

            return user.Id;
        }

        private void AddDefaultUserPermissions(ApplicationUser user)
        {
            var allPerms = repository.PermissionRepository.GetAll();
            List<Permission> permsToAdd = new();

            var bookings = allPerms.Where(x => x.Name.StartsWith("Booking_"));
            var restaurantRead = allPerms.Where(x => x.Name.StartsWith("Restaurant_Read"));
            var scheduleRead = allPerms.Where(x => x.Name.StartsWith("Schedule_Read"));
            var menuCardRead = allPerms.Where(x => x.Name.StartsWith("MenuCard_Read"));
            var menuRead = allPerms.Where(x => x.Name.StartsWith("Menu_Read"));
            var dishRead = allPerms.Where(x => x.Name.StartsWith("Dish_Read"));
            var applicationUserRead = allPerms.Where(x => x.Name.StartsWith("ApplicationUser_Read"));


            permsToAdd.AddRange(bookings);
            permsToAdd.AddRange(restaurantRead);
            permsToAdd.AddRange(menuCardRead);
            permsToAdd.AddRange(menuRead);
            permsToAdd.AddRange(dishRead);
            permsToAdd.AddRange(applicationUserRead);
            permsToAdd.AddRange(scheduleRead);


            foreach (var item in permsToAdd)
            {
                var userPerm = new UserPermission
                {
                    PermissionId = item.Id,
                    UserId = user.Id
                };

                repository.UserPermissions.Add(userPerm);
            }
        }

        private void AddNewUserPermission(ApplicationUser user)
        {
            var newUserPerm = repository.PermissionRepository.Get(1);

            var userPerm = new UserPermission
            {
                PermissionId = newUserPerm.Id,
                UserId = user.Id
            };
            logger.Info("added default permission new user {userperm}", userPerm);

            repository.UserPermissions.Add(userPerm);
        }

        private void IsOwnerPermissions(ApplicationUser user)
        {
            if (user.IsOwner)
            {
                foreach (var perm in repository.PermissionRepository.GetAll().Skip(1))
                {
                    var Userpermission = new UserPermission
                    {
                        PermissionId = perm.Id,
                        UserId = user.Id
                    };
                    repository.UserPermissions.Add(Userpermission);
                }
            }
        }
    }
}