using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.MenuCards
{
    public class EditDishMenuCardCommand : IRequest<int>
    {
        public MutateDishMenuCardDto Model { get; }
        public int Id { get; }
        public int DishId { get; }
        public int RestaurantId { get; }

        public EditDishMenuCardCommand(MutateDishMenuCardDto model, int id, int dishId, int restaurantId)
        {
            Model = model;
            Id = id;
            DishId = dishId;
            RestaurantId = restaurantId;
        }
    }

    public class EditDishMenuCardCommandHandler : IRequestHandler<EditDishMenuCardCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public EditDishMenuCardCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditDishMenuCardCommand request, CancellationToken cancellationToken)
        {
            ValidateModelIds(request);
            var menuCard = await repository.MenuCards.GetMenuCardIncludingDishes(request.Model.MenuCardId, request.Model.RestaurantId);

            logger.Info("trying to edit {@object} with Id: {Id}", menuCard, request.Model.MenuCardId);

            if (menuCard is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }
            var dish = menuCard.Dishes.SingleOrDefault(x => x.Id == request.Model.Dish.Id);
            if (dish is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }

            dish.Category = request.Model.Dish.Category ?? dish.Category;
            dish.Description = request.Model.Dish.Description ?? dish.Description;
            dish.Name = request.Model.Dish.Name ?? dish.Name;
            dish.DishType = request.Model.Dish.DishType ?? dish.DishType;
            if (dish.Price != request.Model.Dish.Price)
            {
                dish.Price = request.Model.Dish.Price;
            }
            repository.Dishes.Update(dish);
            repository.MenuCards.Update(menuCard);

            await repository.CommitAsync();
            logger.Info("updated {@object} with Id: {id}", dish, dish.Id);

            return menuCard.Id;
        }

        private static void ValidateModelIds(EditDishMenuCardCommand request)
        {
            if (request.Model.MenuCardId == 0)
            {
                request.Model.MenuCardId = request.Id;
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