using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Data.Entities.Account;
using Horeca.Shared.Data.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NLog;

namespace Horeca.Core.Handlers.Commands.Restaurants
{
    public class RemoveEmployeeFromRestaurantCommand : IRequest<int>
    {
        public RemoveEmployeeFromRestaurantCommand(int restaurantId, string userId)
        {
            RestaurantId = restaurantId;
            UserId = userId;
        }

        public int RestaurantId { get; }
        public string UserId { get; }
    }

    public class RemoveEmployeeFromRestaurantCommandHandler : IRequestHandler<RemoveEmployeeFromRestaurantCommand, int>
    {
        private readonly IUnitOfWork repository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IApplicationDbContext context;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public RemoveEmployeeFromRestaurantCommandHandler(IUnitOfWork repository, UserManager<ApplicationUser> userManager, IApplicationDbContext context)
        {
            this.repository = repository;
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<int> Handle(RemoveEmployeeFromRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to remove {object} with id: {id} from {resto} with id: {restId}", nameof(ApplicationUser), request.UserId, nameof(Restaurant), request.RestaurantId);
            logger.Info("trying to get {object} with id: {id}", nameof(Restaurants), request.RestaurantId);

            var restaurant = repository.Restaurants.GetRestaurantIncludingDependenciesById(request.RestaurantId);
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
            logger.Info("trying to get {object}", nameof(RestaurantUser));

            RestaurantUser? restaurantUser = restaurant.Employees.Find(x => x.RestaurantId.Equals(restaurant.Id) && x.UserId.Equals(user.Id));
            if (restaurantUser == null)
            {
                logger.Error(EntityNotFoundException.Instance);
                throw new EntityNotFoundException();
            }

            logger.Info("removing employee with id: {id}", request.UserId);

            restaurant.Employees.Remove(restaurantUser);
            context.RestaurantUsers.Remove(restaurantUser);

            await context.SaveChangesAsync(cancellationToken);
            await repository.CommitAsync();

            logger.Info("Deleted {@object} with id {id} from {@dish} with Id: {id}", user, request.UserId, restaurant, request.RestaurantId);

            return restaurant.Id;
        }
    }
}