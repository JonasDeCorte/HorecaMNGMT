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
        private readonly IUnitOfWork repository;

        public DeleteIngredientDishCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteIngredientDishCommand request, CancellationToken cancellationToken)
        {
            var dish = repository.Dishes.GetDishIncludingDependencies(request.Model.DishId);
            var ingredient = repository.Ingredients.Get(request.Model.IngredientId);
            dish.Ingredients.Remove(ingredient);
            repository.Ingredients.Delete(ingredient.Id);
            await repository.CommitAsync();
            return request.Model.IngredientId;
        }
    }
}