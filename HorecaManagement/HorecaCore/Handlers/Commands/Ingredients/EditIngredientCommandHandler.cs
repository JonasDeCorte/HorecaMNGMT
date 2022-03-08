using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos;
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
        private readonly IUnitOfWork _repository;

        public EditIngredientCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(EditIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredient = _repository.Ingredients.GetIngredientIncludingUnit(request.Model.Id);

            if (ingredient is null)
            {
                throw new EntityNotFoundException("Entity does not exist");
            }

            ingredient.IngredientType = request.Model.IngredientType ?? ingredient.IngredientType;
            ingredient.Name = request.Model.Name ?? ingredient.Name;

            if (request.Model.BaseAmount != ingredient.BaseAmount)
                ingredient.BaseAmount = request.Model.BaseAmount;

            ingredient.Unit = request.Model.Unit ?? ingredient.Unit;
            _repository.Units.Update(ingredient.Unit);
            _repository.Ingredients.Update(ingredient);

            await _repository.CommitAsync();

            return request.Model.Id;
        }
    }
}