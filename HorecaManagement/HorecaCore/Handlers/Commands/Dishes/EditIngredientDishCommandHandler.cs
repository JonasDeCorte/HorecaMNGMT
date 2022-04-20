using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Dishes
{
    public class EditIngredientDishCommand : IRequest<int>
    {
        public MutateIngredientByDishDto Model { get; }

        public EditIngredientDishCommand(MutateIngredientByDishDto model)
        {
            Model = model;
        }
    }

    public class EditIngredientDishCommandHandler : IRequestHandler<EditIngredientDishCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public EditIngredientDishCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditIngredientDishCommand request, CancellationToken cancellationToken)
        {
            var dish = await repository.Dishes.GetDishIncludingDependencies(request.Model.Id, request.Model.RestaurantId);

            logger.Info("trying to edit {@object} with Id: {Id}", dish, request.Model.Id);

            if (dish is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }
            DishIngredient? dishIngredient = dish.DishIngredients.SingleOrDefault(x => x.IngredientId == request.Model.Ingredient.Id);
            if (dishIngredient is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }

            dishIngredient.Ingredient.Name = request.Model.Ingredient.Name ?? dishIngredient.Ingredient.Name;
            if (request.Model.Ingredient.BaseAmount != dishIngredient.Ingredient.BaseAmount)
                dishIngredient.Ingredient.BaseAmount = request.Model.Ingredient.BaseAmount;
            dishIngredient.Ingredient.IngredientType = request.Model.Ingredient.IngredientType ?? dishIngredient.Ingredient.IngredientType;

            Shared.Data.Entities.Unit unit = repository.Units.Get(request.Model.Ingredient.Unit.Id);
            logger.Info("check if unit exists in database with id {id} ", request.Model.Ingredient.Unit.Id);

            var modelUnit = new Shared.Data.Entities.Unit
            {
                Name = request.Model.Ingredient.Unit.Name
            };

            dishIngredient.Ingredient.Unit = unit ?? modelUnit;

            repository.Ingredients.Update(dishIngredient.Ingredient);
            repository.Dishes.Update(dish);

            await repository.CommitAsync();
            logger.Info("updated {@object} with Id: {id}", dishIngredient.Ingredient, dishIngredient.Ingredient.Id);

            return dish.Id;
        }
    }
}