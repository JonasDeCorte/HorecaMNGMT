using Horeca.Shared.Data;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.MenuCards
{
    public class DeleteMenuMenuCardCommand : IRequest<int>

    {
        public DeleteMenuMenuCardDto Model { get; set; }
        public int Id { get; }
        public int MenuId { get; }
        public int RestaurantId { get; }

        public DeleteMenuMenuCardCommand(DeleteMenuMenuCardDto model, int id, int menuId, int restaurantId)
        {
            Model = model;
            Id = id;
            MenuId = menuId;
            RestaurantId = restaurantId;
        }
    }

    public class DeleteMenuMenuCardCommandHandler : IRequestHandler<DeleteMenuMenuCardCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteMenuMenuCardCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteMenuMenuCardCommand request, CancellationToken cancellationToken)
        {
            ValidateModelIds(request);
            var menuCard = await repository.MenuCards.GetMenuCardIncludingDependencies(request.Model.MenuCardId, request.Model.RestaurantId);
            var menu = repository.Menus.Get(request.Model.MenuId);
            logger.Info("trying to delete {@object} with id {objId} from {@dish} with Id: {id}", menu, request.Model.MenuId, menuCard, request.Model.MenuCardId);

            menuCard.Menus.Remove(menu);
            repository.Menus.Delete(menu.Id);

            await repository.CommitAsync();

            logger.Info("deleted {@object} with id {objId} from {@dish} with Id: {id}", menu, request.Model.MenuId, menuCard, request.Model.MenuCardId);

            return request.Model.MenuId;
        }

        private static void ValidateModelIds(DeleteMenuMenuCardCommand request)
        {
            if (request.Model.MenuCardId == 0)
            {
                request.Model.MenuCardId = request.Id;
            }
            if (request.Model.MenuId == 0)
            {
                request.Model.MenuId = request.MenuId;
            }
            if (request.Model.RestaurantId == 0)
            {
                request.Model.RestaurantId = request.RestaurantId;
            }
        }
    }
}