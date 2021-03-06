using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Ingredients;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Ingredients
{
    public class EditIngredientCommand : IRequest<int>
    {
        public MutateIngredientDto Model { get; }
        public int Id { get; }
        public int RestaurantId { get; }

        public EditIngredientCommand(MutateIngredientDto model, int id, int restaurantId)
        {
            Model = model;
            Id = id;
            RestaurantId = restaurantId;
        }
    }

    public class EditIngredientCommandHandler : IRequestHandler<EditIngredientCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public EditIngredientCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditIngredientCommand request, CancellationToken cancellationToken)
        {
            ValidateModelIds(request);
            var ingredient = await repository.Ingredients.GetIngredientIncludingUnit(request.Model.Id, request.Model.RestaurantId);

            logger.Info("trying to edit {@object} with Id: {Id}", ingredient, request.Model.Id);

            if (ingredient is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }

            ingredient.IngredientType = request.Model.IngredientType ?? ingredient.IngredientType;
            ingredient.Name = request.Model.Name ?? ingredient.Name;

            if (request.Model.BaseAmount != ingredient.BaseAmount)
            {
                ingredient.BaseAmount = request.Model.BaseAmount;
            }

            if (request.Model.Unit.Id != 0)
            {
                var modelUnit = repository.Units.Get(request.Model.Unit.Id);
                if (modelUnit is null)
                {
                    logger.Error(EntityNotFoundException.Instance);

                    throw new EntityNotFoundException();
                }
                ingredient.Unit = modelUnit ?? ingredient.Unit;
                ingredient.Unit.IsEnabled = true;
            }

            repository.Ingredients.Update(ingredient);

            await repository.CommitAsync();
            logger.Info("updated {@object} with Id: {id}", ingredient, ingredient.Id);

            return ingredient.Id;
        }

        private static void ValidateModelIds(EditIngredientCommand request)
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