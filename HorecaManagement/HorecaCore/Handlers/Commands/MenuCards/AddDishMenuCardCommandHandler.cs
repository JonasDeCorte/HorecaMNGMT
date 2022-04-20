using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.MenuCards
{
    public class AddDishMenuCardCommand : IRequest<int>

    {
        public AddDishMenuCardCommand(MutateDishMenuCardDto model)
        {
            Model = model;
        }

        public MutateDishMenuCardDto Model { get; }
    }

    public class AddDishMenuCardCommandHandler : IRequestHandler<AddDishMenuCardCommand, int>
    {
        private readonly IUnitOfWork repository;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public AddDishMenuCardCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(AddDishMenuCardCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to add {@object} to menucard with Id: {Id}", request.Model.Dish, request.Model.MenuCardId);

            var menuCard = await repository.MenuCards.GetMenuCardIncludingDishes(request.Model.MenuCardId, request.Model.RestaurantId);

            var entity = new Dish
            {
                Name = request.Model.Dish.Name,
                Category = request.Model.Dish.Category,
                Description = request.Model.Dish.Description,
                DishType = request.Model.Dish.DishType,
                Price = request.Model.Dish.Price,
            };

            repository.Dishes.Add(entity);
            menuCard.Dishes.Add(entity);
            repository.MenuCards.Update(menuCard);
            await repository.CommitAsync();

            // now when the entity exists in the db - attach the restaurant as FK
            entity.Restaurant = menuCard.Restaurant;
            repository.Dishes.Update(entity);
            await repository.CommitAsync();
            logger.Info("succes adding {@object} to menucard with id {id}", entity, menuCard.Id);

            return entity.Id;
        }
    }
}