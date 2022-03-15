using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Ingredients;
using MediatR;

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

        public EditIngredientCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredient = repository.Ingredients.GetIngredientIncludingUnit(request.Model.Id);

            if (ingredient is null)
            {
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

            return request.Model.Id;
        }
    }
}