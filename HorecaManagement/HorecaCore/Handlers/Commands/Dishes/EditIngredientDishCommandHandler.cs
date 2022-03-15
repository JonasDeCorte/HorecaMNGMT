using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
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
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public EditIngredientDishCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditIngredientDishCommand request, CancellationToken cancellationToken)
        {
            var dish = repository.Dishes.GetDishIncludingDependencies(request.Model.Id);

            logger.Info("trying to edit {@object} with Id: {Id}", dish, request.Model.Id);

            if (dish is null)
            {
                logger.Error("{Object} with Id: {id} does not exist", nameof(dish), request.Model.Id);

                throw new EntityNotFoundException("Dish does not exist");
            }
            var ingredient = dish.Ingredients.SingleOrDefault(x => x.Id == request.Model.Ingredient.Id);
            if (ingredient is null)
            {
                logger.Error("{Object} with Id: {id} does not exist", nameof(ingredient), request.Model.Ingredient.Id);

                throw new EntityNotFoundException("Ingredient does not exist");
            }

            ingredient.Name = request.Model.Ingredient.Name ?? ingredient.Name;
            if (request.Model.Ingredient.BaseAmount != ingredient.BaseAmount)
                ingredient.BaseAmount = request.Model.Ingredient.BaseAmount;
            ingredient.IngredientType = request.Model.Ingredient.IngredientType ?? ingredient.IngredientType;
            var modelUnit = new Horeca.Shared.Data.Entities.Unit
            {
                Name = request.Model.Ingredient.Unit.Name
            };
            ingredient.Unit = modelUnit ?? ingredient.Unit;

            repository.Ingredients.Update(ingredient);
            repository.Dishes.Update(dish);

            await repository.CommitAsync();
            logger.Info("updated {@object} with Id: {id}", ingredient, ingredient.Id);

            return dish.Id;
        }
    }
}