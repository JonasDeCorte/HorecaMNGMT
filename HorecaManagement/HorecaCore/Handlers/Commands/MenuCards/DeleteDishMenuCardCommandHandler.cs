using Horeca.Shared.Data;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.MenuCards
{
    public class DeleteDishMenuCardCommand : IRequest<int>

    {
        public DeleteDishMenuCardDto Model { get; set; }
        public int Id { get; }
        public int DishId { get; }
        public int RestaurantId { get; }

        public DeleteDishMenuCardCommand(DeleteDishMenuCardDto model, int id, int dishId, int restaurantId)
        {
            Model = model;
            Id = id;
            DishId = dishId;
            RestaurantId = restaurantId;
        }
    }

    public class DeleteDishMenuCardCommandHandler : IRequestHandler<DeleteDishMenuCardCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteDishMenuCardCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteDishMenuCardCommand request, CancellationToken cancellationToken)
        {
            ValidateModelIds(request);
            var menuCard = await repository.MenuCards.GetMenuCardIncludingDependencies(request.Model.MenuCardId, request.Model.RestaurantId);
            var dish = repository.Dishes.Get(request.Model.DishId);

            logger.Info("trying to delete {@object} with id {objId} from {@dish} with Id: {id}", dish, request.Model.DishId, menuCard, request.Model.MenuCardId);

            menuCard.Dishes.Remove(dish);
            repository.Dishes.Delete(dish.Id);

            await repository.CommitAsync();
            logger.Info("deleted {@object} with id {objId} from {@dish} with Id: {id}", dish, request.Model.DishId, menuCard, request.Model.MenuCardId);

            return request.Model.DishId;
        }

        private static void ValidateModelIds(DeleteDishMenuCardCommand request)
        {
            if (request.Model.MenuCardId == 0)
            {
                request.Model.MenuCardId = request.Id;
            }
            if (request.Model.DishId == 0)
            {
                request.Model.DishId = request.DishId;
            }
            if (request.Model.RestaurantId == 0)
            {
                request.Model.RestaurantId = request.RestaurantId;
            }
        }
    }
}