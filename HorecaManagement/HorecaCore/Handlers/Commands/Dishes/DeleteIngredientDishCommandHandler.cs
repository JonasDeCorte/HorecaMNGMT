using Horeca.Shared.Data;
using Horeca.Shared.Data.Services;
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
        private readonly IApplicationDbContext context;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteIngredientDishCommandHandler(IUnitOfWork repository, IApplicationDbContext context)
        {
            this.repository = repository;
            this.context = context;
        }

        public async Task<int> Handle(DeleteIngredientDishCommand request, CancellationToken cancellationToken)
        {
            var dish = await repository.Dishes.GetDishIncludingDependencies(request.Model.DishId, request.Model.RestaurantId);

            var ingredient = repository.Ingredients.Get(request.Model.IngredientId);
            logger.Info("trying to delete {@object} with id {objId} from {@dish} with Id: {id}", ingredient, request.Model.IngredientId, dish, request.Model.DishId);
            var dishIngred = dish.DishIngredients.Find(x => x.IngredientId == ingredient.Id);

            dish.DishIngredients.Remove(dishIngred);
            context.DishIngredients.Remove(dishIngred);

            await context.SaveChangesAsync(cancellationToken);

            await repository.CommitAsync();

            logger.Info("Deleted {@object} with id {ingredId} from {@dish} with Id: {id}", ingredient, request.Model.IngredientId, dish, request.Model.DishId);

            return ingredient.Id;
        }
    }
}