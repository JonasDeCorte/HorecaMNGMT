using Horeca.Shared.Data;
using Horeca.Shared.Data.Services;
using Horeca.Shared.Dtos.Menus;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Menus
{
    public class DeleteDishMenuCommand : IRequest<int>

    {
        public DeleteDishMenuDto Model { get; set; }
        public int Id { get; }
        public int DishId { get; }
        public int RestaurantId { get; }

        public DeleteDishMenuCommand(DeleteDishMenuDto model, int id, int dishId, int restaurantId)
        {
            Model = model;
            Id = id;
            DishId = dishId;
            RestaurantId = restaurantId;
        }
    }

    public class DeleteDishMenuCommandHandler : IRequestHandler<DeleteDishMenuCommand, int>
    {
        private readonly IUnitOfWork repository;
        private readonly IApplicationDbContext context;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteDishMenuCommandHandler(IUnitOfWork repository, IApplicationDbContext context)
        {
            this.repository = repository;
            this.context = context;
        }

        public async Task<int> Handle(DeleteDishMenuCommand request, CancellationToken cancellationToken)
        {
            ValidateModelIds(request);
            var menu = await repository.Menus.GetMenuIncludingDependencies(request.Model.MenuId, request.Model.RestaurantId);
            var dish = repository.Dishes.Get(request.Model.DishId);

            logger.Info("trying to delete {@object} with id {objId} from {@dish} with Id: {id}", dish, request.Model.DishId, menu, request.Model.MenuId);
            var menuDish = menu.MenuDishes.Find(x => x.DishId == dish.Id);
            menu.MenuDishes.Remove(menuDish);
            context.MenuDishes.Remove(menuDish);

            await context.SaveChangesAsync(cancellationToken);
            await repository.CommitAsync();

            logger.Info("deleted {@object} with id {objId} from {@dish} with Id: {id}", dish, request.Model.DishId, menu, request.Model.MenuId);

            return request.Model.DishId;
        }

        private static void ValidateModelIds(DeleteDishMenuCommand request)
        {
            if (request.Model.MenuId == 0)
            {
                request.Model.MenuId = request.Id;
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