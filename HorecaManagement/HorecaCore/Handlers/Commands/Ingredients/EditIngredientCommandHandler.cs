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

        public EditIngredientCommand(MutateIngredientDto model)
        {
            Model = model;
        }
    }

    public class EditIngredientCommandHandler : IRequestHandler<EditIngredientCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public EditIngredientCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredient = repository.Ingredients.GetIngredientIncludingUnit(request.Model.Id);

            logger.Info("trying to edit {@object} with Id: {Id}", ingredient, request.Model.Id);

            if (ingredient is null)
            {
                logger.Error("{Object} with Id: {id} does not exist", nameof(ingredient), request.Model.Id);

                throw new EntityNotFoundException("Entity does not exist");
            }

            ingredient.IngredientType = request.Model.IngredientType ?? ingredient.IngredientType;
            ingredient.Name = request.Model.Name ?? ingredient.Name;

            if (request.Model.BaseAmount != ingredient.BaseAmount)
                ingredient.BaseAmount = request.Model.BaseAmount;

            var modelUnit = repository.Units.Get(request.Model.Unit.Id);
            modelUnit.Name = request.Model.Unit.Name ?? modelUnit.Name;

            ingredient.Unit = modelUnit ?? ingredient.Unit;
            ingredient.Unit.IsEnabled = true;

            repository.Units.Update(modelUnit);
            repository.Ingredients.Update(ingredient);

            await repository.CommitAsync();
            logger.Info("updated {@object} with Id: {id}", ingredient, ingredient.Id);

            return request.Model.Id;
        }
    }
}