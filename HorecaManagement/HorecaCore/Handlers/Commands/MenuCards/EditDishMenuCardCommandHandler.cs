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

        public EditDishMenuCardCommand(MutateDishMenuCardDto model)
        {
            Model = model;
        }
    }

    public class EditDishMenuCardCommandHandler : IRequestHandler<EditDishMenuCardCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public EditDishMenuCardCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditDishMenuCardCommand request, CancellationToken cancellationToken)
        {
            var menuCard = repository.MenuCards.GetMenuCardIncludingDependencies(request.Model.MenuCardId);

            logger.Info("trying to edit {@object} with Id: {Id}", menuCard, request.Model.MenuCardId);

            if (menuCard is null)
            {
                logger.Error("{Object} with Id: {id} does not exist", nameof(menuCard), request.Model.MenuCardId);

                throw new EntityNotFoundException("MenuCard does not exist");
            }
            var dish = menuCard.Dishes.SingleOrDefault(x => x.Id == request.Model.Dish.Id);
            if (dish is null)
            {
                logger.Error("{Object} with Id: {id} does not exist", nameof(dish), request.Model.Dish.Id);

                throw new EntityNotFoundException("Dish does not exist");
            }

            dish.Category = request.Model.Dish.Category ?? dish.Category;
            dish.Description = request.Model.Dish.Description ?? dish.Description;
            dish.Name = request.Model.Dish.Name ?? dish.Name;
            dish.DishType = request.Model.Dish.DishType ?? dish.DishType;

            repository.Dishes.Update(dish);
            repository.MenuCards.Update(menuCard);

            await repository.CommitAsync();
            logger.Info("updated {@object} with Id: {id}", dish, dish.Id);

            return menuCard.Id;
        }
    }
}