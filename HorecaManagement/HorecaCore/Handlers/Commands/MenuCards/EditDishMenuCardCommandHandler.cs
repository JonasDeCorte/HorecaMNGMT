using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;

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

        public EditDishMenuCardCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditDishMenuCardCommand request, CancellationToken cancellationToken)
        {
            var menuCard = repository.MenuCards.GetMenuCardIncludingDependencies(request.Model.MenuCardId);

            if (menuCard is null)
            {
                throw new EntityNotFoundException("MenuCard does not exist");
            }
            var Dish = menuCard.Dishes.SingleOrDefault(x => x.Id == request.Model.Dish.Id);
            if (Dish is null)
            {
                throw new EntityNotFoundException("Dish does not exist");
            }

            Dish.Category = request.Model.Dish.Category ?? Dish.Category;
            Dish.Description = request.Model.Dish.Description ?? Dish.Description;
            Dish.Name = request.Model.Dish.Name ?? Dish.Name;
            Dish.DishType = request.Model.Dish.DishType ?? Dish.DishType;

            repository.Dishes.Update(Dish);
            repository.MenuCards.Update(menuCard);

            await repository.CommitAsync();

            return menuCard.Id;
        }
    }
}