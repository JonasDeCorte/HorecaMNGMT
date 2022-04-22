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
        public int Id { get; }
        public int IngredientId { get; }
        public int RestaurantId { get; }

        public DeleteIngredientDishCommand(DeleteIngredientDishDto model, int id, int ingredientId, int restaurantId)
        {
            Model = model;
            Id = id;
            IngredientId = ingredientId;
            RestaurantId = restaurantId;
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
            ValidateRequestIds(request);
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

        private static void ValidateRequestIds(DeleteIngredientDishCommand request)
        {
            if (request.Model.DishId == 0)
            {
                request.Model.DishId = request.Id;
            }
            if (request.Model.IngredientId == 0)
            {
                request.Model.IngredientId = request.IngredientId;
            }
            if (request.Model.RestaurantId == 0)
            {
                request.Model.RestaurantId = request.RestaurantId;
            }
        }
    }
}