using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;

namespace Horeca.Core.Handlers.Commands.MenuCards
{
    public class EditMenuMenuCardCommand : IRequest<int>
    {
        public MutateMenuMenuCardDto Model { get; }

        public EditMenuMenuCardCommand(MutateMenuMenuCardDto model)
        {
            Model = model;
        }
    }

    public class EditMenuMenuCardCommandHandler : IRequestHandler<EditMenuMenuCardCommand, int>
    {
        private readonly IUnitOfWork repository;

        public EditMenuMenuCardCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditMenuMenuCardCommand request, CancellationToken cancellationToken)
        {
            var menuCard = repository.MenuCards.GetMenuCardIncludingDependencies(request.Model.MenuCardId);

            if (menuCard is null)
            {
                throw new EntityNotFoundException("MenuCard does not exist");
            }
            var menu = menuCard.Menus.SingleOrDefault(x => x.Id == request.Model.Menu.Id);
            if (menu is null)
            {
                throw new EntityNotFoundException("Menu does not exist");
            }

            menu.Category = request.Model.Menu.Category ?? menu.Category;
            menu.Description = request.Model.Menu.Description ?? menu.Description;
            menu.Name = request.Model.Menu.Name ?? menu.Name;

            repository.Menus.Update(menu);
            repository.MenuCards.Update(menuCard);

            await repository.CommitAsync();

            return menuCard.Id;
        }
    }
}