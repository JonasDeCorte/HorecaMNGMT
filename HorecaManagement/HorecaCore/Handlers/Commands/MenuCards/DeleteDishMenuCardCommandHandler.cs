using Horeca.Shared.Data;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;

namespace Horeca.Core.Handlers.Commands.MenuCards
{
    public class DeleteDishMenuCardCommand : IRequest<int>

    {
        public DeleteDishMenuCardDto Model { get; set; }

        public DeleteDishMenuCardCommand(DeleteDishMenuCardDto model)
        {
            Model = model;
        }
    }

    public class DeleteDishMenuCardCommandHandler : IRequestHandler<DeleteDishMenuCardCommand, int>
    {
        private readonly IUnitOfWork repository;

        public DeleteDishMenuCardCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteDishMenuCardCommand request, CancellationToken cancellationToken)
        {
            var menuCard = repository.MenuCards.GetMenuCardIncludingDependencies(request.Model.MenuCardId);
            var menu = repository.Dishes.Get(request.Model.DishId);
            menuCard.Dishes.Remove(menu);
            repository.Dishes.Delete(menu.Id);
            await repository.CommitAsync();
            return request.Model.DishId;
        }
    }
}