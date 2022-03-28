using Horeca.Shared.Data;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;
using NLog;

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
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteDishMenuCardCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteDishMenuCardCommand request, CancellationToken cancellationToken)
        {
            var menuCard = repository.MenuCards.GetMenuCardIncludingDependencies(request.Model.MenuCardId);
            var dish = repository.Dishes.Get(request.Model.DishId);

            logger.Info("trying to delete {@object} with id {objId} from {@dish} with Id: {id}", dish, request.Model.DishId, menuCard, request.Model.MenuCardId);

            menuCard.Dishes.Remove(dish);
            repository.Dishes.Delete(dish.Id);

            await repository.CommitAsync();
            logger.Info("deleted {@object} with id {objId} from {@dish} with Id: {id}", dish, request.Model.DishId, menuCard, request.Model.MenuCardId);

            return request.Model.DishId;
        }
    }
}