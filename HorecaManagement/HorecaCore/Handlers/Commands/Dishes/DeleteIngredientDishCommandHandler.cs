using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Dishes;
using MediatR;
using NLog;

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
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteIngredientDishCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteIngredientDishCommand request, CancellationToken cancellationToken)
        {
            var dish = repository.Dishes.GetDishIncludingDependencies(request.Model.DishId);

            var ingredient = repository.Ingredients.Get(request.Model.IngredientId);
            logger.Info("trying to delete {@object} with id {objId} from {@dish} with Id: {id}", ingredient, request.Model.IngredientId, dish, request.Model.DishId);

            dish.Ingredients.Remove(ingredient);
            repository.Ingredients.Delete(ingredient.Id);
            await repository.CommitAsync();

            logger.Info("Deleted {@object} with id {ingredId} from {@dish} with Id: {id}", ingredient, request.Model.IngredientId, dish, request.Model.DishId);

            return ingredient.Id;
        }
    }
}