using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Dishes
{
    public class AddIngredientDishCommand : IRequest<int>
    {
        public MutateIngredientByDishDto Model { get; }
        public int Id { get; }
        public int RestaurantId { get; }

        public AddIngredientDishCommand(MutateIngredientByDishDto model, int id, int restaurantId)
        {
            Model = model;
            Id = id;
            RestaurantId = restaurantId;
        }
    }

    public class AddIngredientDishCommandHandler : IRequestHandler<AddIngredientDishCommand, int>
    {
        private readonly IUnitOfWork repository;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public AddIngredientDishCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(AddIngredientDishCommand request, CancellationToken cancellationToken)
        {
            ValidateModelIds(request);
            logger.Info("trying to add {@object} to Dish with Id: {Id}", request.Model.Ingredient, request.Model.Id);
            var dish = await repository.Dishes.GetDishIncludingDependencies(request.Model.Id, request.Model.RestaurantId);

            Ingredient entity;
            if (request.Model.Ingredient.Id == 0)
            {
                Shared.Data.Entities.Unit unit = repository.Units.Get(request.Model.Ingredient.Unit.Id);

                logger.Info("check if unit exists in database with id {id} ", request.Model.Ingredient.Unit.Id);

                entity = new Ingredient
                {
                    Name = request.Model.Ingredient.Name,
                    BaseAmount = request.Model.Ingredient.BaseAmount,
                    IngredientType = request.Model.Ingredient.IngredientType,
                    Unit = unit ?? new Shared.Data.Entities.Unit
                    {
                        Name = request.Model.Ingredient.Unit.Name,
                    },
                };
            }
            else
            {
                logger.Info("ingredients exists, get ingredient from database  {id} ", request.Model.Ingredient.Id);

                entity = await repository.Ingredients.GetIngredientIncludingUnit(request.Model.Ingredient.Id, request.Model.Ingredient.RestaurantId);
                logger.Info("ingredients exists, get ingredient from database  {@entity} ", entity);
            }

            dish.DishIngredients.Add(new DishIngredient()
            {
                Ingredient = entity,
                Dish = dish,
            });

            repository.Dishes.Update(dish);
            await repository.CommitAsync();
            // now when the entity exists in the db - attach the restaurant as FK
            entity.Restaurant = dish.Restaurant;
            entity.Unit.Restaurant = dish.Restaurant;
            repository.Ingredients.Update(entity);
            await repository.CommitAsync();

            logger.Info("succes adding {@object} to dish with id {id}", entity, dish.Id);

            return entity.Id;
        }

        private static void ValidateModelIds(AddIngredientDishCommand request)
        {
            if (request.Model.Id == 0)
            {
                request.Model.Id = request.Id;
            }
            if (request.Model.RestaurantId == 0)
            {
                request.Model.RestaurantId = request.RestaurantId;
            }
        }
    }
}