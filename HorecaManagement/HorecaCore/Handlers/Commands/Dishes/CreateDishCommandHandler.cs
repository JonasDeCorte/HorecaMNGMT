using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Dishes
{
    public class CreateDishCommand : IRequest<int>
    {
        public MutateDishDto Model { get; }
        public int RestaurantId { get; }

        public CreateDishCommand(MutateDishDto model, int restaurantId)
        {
            Model = model;
            RestaurantId = restaurantId;
        }

        public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, int>
        {
            private readonly IUnitOfWork repository;
            private static readonly Logger logger = LogManager.GetCurrentClassLogger();

            public CreateDishCommandHandler(IUnitOfWork repository)
            {
                this.repository = repository;
            }

            public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
            {
                ValidateModelIds(request);
                logger.Info("trying to create {object}", nameof(Dish));
                var restaurant = repository.Restaurants.Get(request.Model.RestaurantId);

                if (restaurant == null)
                {
                    logger.Error(EntityNotFoundException.Instance);

                    throw new EntityNotFoundException();
                }

                var entity = new Dish
                {
                    Name = request.Model.Name,
                    Category = request.Model.Category,
                    Description = request.Model.Description,
                    DishType = request.Model.DishType,
                    Price = request.Model.Price,
                };

                repository.Dishes.Add(entity);
                await repository.CommitAsync();

                // now when the entity exists in the db - attach the restaurant as FK
                entity.Restaurant = restaurant;
                repository.Dishes.Update(entity);
                await repository.CommitAsync();
                logger.Info("adding {@object} with id {id}", entity, entity.Id);

                return entity.Id;
            }

            private static void ValidateModelIds(CreateDishCommand request)
            {
                if (request.Model.RestaurantId == 0)
                {
                    request.Model.RestaurantId = request.RestaurantId;
                }
            }
        }
    }
}