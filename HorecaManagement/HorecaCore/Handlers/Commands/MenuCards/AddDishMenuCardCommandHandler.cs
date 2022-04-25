using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.MenuCards
{
    public class AddDishMenuCardCommand : IRequest<int>

    {
        public AddDishMenuCardCommand(MutateDishMenuCardDto model, int id, int restaurantId)
        {
            Model = model;
            Id = id;
            RestaurantId = restaurantId;
        }

        public MutateDishMenuCardDto Model { get; }
        public int Id { get; }
        public int RestaurantId { get; }
    }

    public class AddDishMenuCardCommandHandler : IRequestHandler<AddDishMenuCardCommand, int>
    {
        private readonly IUnitOfWork repository;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public AddDishMenuCardCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(AddDishMenuCardCommand request, CancellationToken cancellationToken)
        {
            ValidateModelIds(request);
            logger.Info("trying to add {@object} to menucard with Id: {Id}", request.Model.Dish, request.Model.MenuCardId);

            var menuCard = await repository.MenuCards.GetMenuCardIncludingDishes(request.Model.MenuCardId, request.Model.RestaurantId);
            Dish entity;
            if (request.Model.Dish.Id == 0)
            {
                entity = new Dish
                {
                    Name = request.Model.Dish.Name,
                    Category = request.Model.Dish.Category,
                    Description = request.Model.Dish.Description,
                    DishType = request.Model.Dish.DishType,
                    Price = request.Model.Dish.Price,
                };
            }
            else
            {
                logger.Info("dish exists, get dish   from database  {id} ", request.Model.Dish.Id);
                entity = await repository.Dishes.GetDishById(request.Model.Dish.Id, request.Model.Dish.RestaurantId);
                if (entity == null)
                {
                    logger.Error(EntityNotFoundException.Instance);
                    throw new EntityNotFoundException();
                }
                logger.Info("check if menuCard contains dish with id {id}", entity.Id);
                var existingDish = menuCard.Dishes.SingleOrDefault(x => x.Id.Equals(entity.Id), null);

                if (existingDish != null)
                {
                    logger.Error(EntityIsAlreadyPartOfThisCollectionException.Instance);
                    throw new EntityIsAlreadyPartOfThisCollectionException();
                }
            }

            menuCard.Dishes.Add(entity);
            repository.MenuCards.Update(menuCard);
            await repository.CommitAsync();

            // now when the entity exists in the db - attach the restaurant as FK
            entity.Restaurant = menuCard.Restaurant;
            repository.Dishes.Update(entity);
            await repository.CommitAsync();
            logger.Info("succes adding {@object} to menucard with id {id}", entity, menuCard.Id);

            return entity.Id;
        }

        private static void ValidateModelIds(AddDishMenuCardCommand request)
        {
            if (request.Model.MenuCardId == 0)
            {
                request.Model.MenuCardId = request.Id;
            }
            if (request.Model.RestaurantId == 0)
            {
                request.Model.RestaurantId = request.RestaurantId;
            }
        }
    }
}