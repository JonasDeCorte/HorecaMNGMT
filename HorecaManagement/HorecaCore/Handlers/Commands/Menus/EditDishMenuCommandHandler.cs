using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Menus;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Menus
{
    public class EditDishMenuCommand : IRequest<int>
    {
        public MutateDishMenuDto Model { get; }
        public int Id { get; }
        public int DishId { get; }
        public int RestaurantId { get; }

        public EditDishMenuCommand(MutateDishMenuDto model, int id, int dishId, int restaurantId)
        {
            Model = model;
            Id = id;
            DishId = dishId;
            RestaurantId = restaurantId;
        }
    }

    public class EditIngredientDishCommandHandler : IRequestHandler<EditDishMenuCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public EditIngredientDishCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditDishMenuCommand request, CancellationToken cancellationToken)
        {
            ValidateModelIds(request);
            var menu = await repository.Menus.GetMenuIncludingDependencies(request.Model.Id, request.Model.RestaurantId);

            logger.Info("trying to edit {@object} with Id: {Id}", menu, request.Model.Id);

            if (menu is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }
            var menuDish = menu.MenuDishes.SingleOrDefault(x => x.Id == request.Model.Dish.Id);

            if (menuDish is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }

            menuDish.Dish.Name = request.Model.Dish.Name ?? menuDish.Dish.Name;
            menuDish.Dish.DishType = request.Model.Dish.DishType ?? menuDish.Dish.DishType;
            menuDish.Dish.Category = request.Model.Dish.Category ?? menuDish.Dish.Category;
            menuDish.Dish.Description = request.Model.Dish.Description ?? menuDish.Dish.Description;
            if (menuDish.Dish.Price != request.Model.Dish.Price)
            {
                menuDish.Dish.Price = request.Model.Dish.Price;
            }
            repository.Dishes.Update(menuDish.Dish);
            repository.Menus.Update(menu);

            await repository.CommitAsync();
            logger.Info("updated {object} with Id: {id}", menuDish, menuDish.Id);

            return menuDish.Id;
        }

        private static void ValidateModelIds(EditDishMenuCommand request)
        {
            if (request.Model.Id == 0)
            {
                request.Model.Id = request.Id;
            }
            if (request.Model.Dish.Id == 0)
            {
                request.Model.Dish.Id = request.DishId;
            }
            if (request.Model.RestaurantId == 0)
            {
                request.Model.RestaurantId = request.RestaurantId;
            }
        }
    }
}