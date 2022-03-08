using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Dishes;
using MediatR;

namespace Horeca.Core.Handlers.Commands.Dishes
{
    public class DeleteIngredientDishCommand : IRequest<int>

    {
        public DeleteIngredientDishDto Model { get; set; }

        public DeleteIngredientDishCommand(DeleteIngredientDishDto model)
        {
            Model = model;
        }
    }

    public class DeleteIngredientDishCommandHandler : IRequestHandler<DeleteIngredientDishCommand, int>
    {
        private readonly IUnitOfWork _repository;

        public DeleteIngredientDishCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(DeleteIngredientDishCommand request, CancellationToken cancellationToken)
        {
            var dish = _repository.Dishes.GetDishIncludingDependencies(request.Model.DishId);
            var ingredient = _repository.Ingredients.Get(request.Model.IngredientId);
            dish.Ingredients.Remove(ingredient);
            _repository.Ingredients.Delete(ingredient.Id);
            await _repository.CommitAsync();
            return request.Model.IngredientId;
        }
    }
}