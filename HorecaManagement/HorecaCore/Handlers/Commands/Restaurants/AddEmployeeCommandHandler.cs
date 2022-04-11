using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Entities.Account;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NLog;

namespace Horeca.Core.Handlers.Commands.Restaurants
{
    public class AddEmployeeCommand : IRequest<int>
    {
        public AddEmployeeCommand(string userId, int restaurantId)
        {
            UserId = userId;
            RestaurantId = restaurantId;
        }

        public string UserId { get; }
        public int RestaurantId { get; }
    }

    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, int>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public AddEmployeeCommandHandler(UserManager<ApplicationUser> userManager, IUnitOfWork repository)
        {
            this.userManager = userManager;
            this.repository = repository;
        }

        public async Task<int> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to add {object} with id: {id} to {resto} with id: {restId}", nameof(ApplicationUser), request.UserId, nameof(Restaurant), request.RestaurantId);

            logger.Info("trying to get {object} with id: {id}", nameof(Restaurants), request.RestaurantId);

            var restaurant = await repository.Restaurants.GetRestaurantIncludingDependenciesById(request.RestaurantId);
            if (restaurant == null)
            {
                logger.Error(EntityNotFoundException.Instance);
                throw new EntityNotFoundException();
            }
            logger.Info("trying to get {object} with id: {id}", nameof(ApplicationUser), request.UserId);

            var user = await userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                logger.Error(UserNotFoundException.Instance);
                throw new UserNotFoundException();
            }
            logger.Info("adding employee with id: {id}", request.UserId);

            restaurant.Employees.Add(new RestaurantUser
            {
                User = user,
                Restaurant = restaurant
            });
            repository.Restaurants.Update(restaurant);
            await repository.CommitAsync();
            return restaurant.Id;
        }
    }
}