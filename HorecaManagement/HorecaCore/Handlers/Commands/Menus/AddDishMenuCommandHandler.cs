using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Dishes;
using Horeca.Shared.Dtos.Menus;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Menus
{
    public class AddDishMenuCommand : IRequest<int>
    {
        public AddDishMenuCommand(MutateDishMenuDto model)
        {
            Model = model;
        }

        public MutateDishMenuDto Model { get; }
    }

    public class AddDishMenuCommandHandler : IRequestHandler<AddDishMenuCommand, int>
    {
        private readonly IUnitOfWork repository;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public AddDishMenuCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(AddDishMenuCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to add {@object} to menu with Id: {Id}", request.Model.Dish, request.Model.Id);

            var menu = await repository.Menus.GetMenuIncludingDependencies(request.Model.Id, request.Model.RestaurantId);

            var entity = new Dish
            {
                Name = request.Model.Dish.Name,
                Category = request.Model.Dish.Category,
                Description = request.Model.Dish.Description,
                DishType = request.Model.Dish.DishType,
                Price = request.Model.Dish.Price,
            };

            menu.Dishes.Add(entity);
            repository.Dishes.Add(entity);
            repository.Menus.Update(menu);
            await repository.CommitAsync();

            // now when the entity exists in the db - attach the restaurant as FK
            entity.Restaurant = menu.Restaurant;
            repository.Dishes.Update(entity);
            await repository.CommitAsync();
            logger.Info("succes adding {@object} to menu with id {id}", entity, menu.Id);

            return entity.Id;
        }
    }
}