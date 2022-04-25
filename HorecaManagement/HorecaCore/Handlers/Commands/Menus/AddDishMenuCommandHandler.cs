using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Menus;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Menus
{
    public class AddDishMenuCommand : IRequest<int>
    {
        public AddDishMenuCommand(MutateDishMenuDto model, int id, int restaurantId)
        {
            Model = model;
            Id = id;
            RestaurantId = restaurantId;
        }

        public MutateDishMenuDto Model { get; }
        public int Id { get; }
        public int RestaurantId { get; }
    }

    public class AddDishMenuCommandHandler : IRequestHandler<AddDishMenuCommand, int>
    {
        private readonly IUnitOfWork repository;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public AddDishMenuCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(AddDishMenuCommand request, CancellationToken cancellationToken)
        {
            ValidateModelIds(request);
            logger.Info("trying to add {@object} to menu with Id: {Id}", request.Model.Dish, request.Model.Id);
            var menu = await repository.Menus.GetMenuIncludingDependencies(request.Model.Id, request.Model.RestaurantId);
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
                logger.Info("check if menu contains dish with id {id}", entity.Id);
                var existingDish = menu.Dishes.SingleOrDefault(x => x.Id.Equals(entity.Id), null);

                if (existingDish != null)
                {
                    logger.Error(EntityIsAlreadyPartOfThisCollectionException.Instance);
                    throw new EntityIsAlreadyPartOfThisCollectionException();
                }
            }

            menu.Dishes.Add(entity);
            repository.Menus.Update(menu);
            await repository.CommitAsync();

            // now when the entity exists in the db - attach the restaurant as FK
            entity.Restaurant = menu.Restaurant;
            repository.Dishes.Update(entity);
            await repository.CommitAsync();
            logger.Info("succes adding {@object} to menu with id {id}", entity, menu.Id);

            return entity.Id;
        }

        private static void ValidateModelIds(AddDishMenuCommand request)
        {
            if (request.Model.RestaurantId == 0)
            {
                request.Model.RestaurantId = request.RestaurantId;
            }
            if (request.Model.Dish.Id == 0)
            {
                request.Model.Dish.Id = request.Id;
            }
        }
    }
}