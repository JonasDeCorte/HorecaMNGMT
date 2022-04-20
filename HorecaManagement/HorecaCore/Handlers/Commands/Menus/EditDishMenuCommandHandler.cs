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

        public EditDishMenuCommand(MutateDishMenuDto model)
        {
            Model = model;
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
            var menu = await repository.Menus.GetMenuIncludingDependencies(request.Model.Id, request.Model.RestaurantId);

            logger.Info("trying to edit {@object} with Id: {Id}", menu, request.Model.Id);

            if (menu is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }
            var dish = menu.Dishes.SingleOrDefault(x => x.Id == request.Model.Dish.Id);

            if (dish is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }

            dish.Name = request.Model.Dish.Name ?? dish.Name;
            dish.DishType = request.Model.Dish.DishType ?? dish.DishType;
            dish.Category = request.Model.Dish.Category ?? dish.Category;
            dish.Description = request.Model.Dish.Description ?? dish.Description;
            if (dish.Price != request.Model.Dish.Price)
            {
                dish.Price = request.Model.Dish.Price;
            }
            repository.Dishes.Update(dish);
            repository.Menus.Update(menu);

            await repository.CommitAsync();
            logger.Info("updated {@object} with Id: {id}", dish, dish.Id);

            return dish.Id;
        }
    }
}