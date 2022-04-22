using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.MenuCards
{
    public class EditMenuMenuCardCommand : IRequest<int>
    {
        public MutateMenuMenuCardDto Model { get; }
        public int Id { get; }
        public int MenuId { get; }
        public int RestaurantId { get; }

        public EditMenuMenuCardCommand(MutateMenuMenuCardDto model, int id, int menuId, int restaurantId)
        {
            Model = model;
            Id = id;
            MenuId = menuId;
            RestaurantId = restaurantId;
        }
    }

    public class EditMenuMenuCardCommandHandler : IRequestHandler<EditMenuMenuCardCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public EditMenuMenuCardCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditMenuMenuCardCommand request, CancellationToken cancellationToken)
        {
            ValidateModelIds(request);
            var menuCard = await repository.MenuCards.GetMenuCardIncludingMenus(request.Model.MenuCardId, request.Model.RestaurantId);
            logger.Info("trying to edit {@object} with Id: {Id}", menuCard, request.Model.MenuCardId);

            if (menuCard is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }
            var menu = menuCard.Menus.SingleOrDefault(x => x.Id == request.Model.Menu.Id);
            if (menu is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }

            menu.Category = request.Model.Menu.Category ?? menu.Category;
            menu.Description = request.Model.Menu.Description ?? menu.Description;
            menu.Name = request.Model.Menu.Name ?? menu.Name;
            if (menu.Price != request.Model.Menu.Price)
            {
                menu.Price = request.Model.Menu.Price;
            }
            repository.Menus.Update(menu);
            repository.MenuCards.Update(menuCard);

            await repository.CommitAsync();
            logger.Info("updated {@object} with Id: {id}", menu, menu.Id);

            return menuCard.Id;
        }

        private static void ValidateModelIds(EditMenuMenuCardCommand request)
        {
            if (request.Model.MenuCardId == 0)
            {
                request.Model.MenuCardId = request.Id;
            }
            if (request.Model.Menu.Id == 0)
            {
                request.Model.Menu.Id = request.MenuId;
            }
            if (request.Model.RestaurantId == 0)
            {
                request.Model.RestaurantId = request.RestaurantId;
            }
        }
    }
}