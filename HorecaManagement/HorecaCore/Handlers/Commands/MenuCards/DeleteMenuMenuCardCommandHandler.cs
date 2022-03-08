using Horeca.Shared.Data;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;

namespace Horeca.Core.Handlers.Commands.MenuCards
{
    public class DeleteMenuMenuCardCommand : IRequest<int>

    {
        public DeleteMenuMenuCardDto Model { get; set; }

        public DeleteMenuMenuCardCommand(DeleteMenuMenuCardDto model)
        {
            Model = model;
        }
    }

    public class DeleteMenuMenuCardCommandHandler : IRequestHandler<DeleteMenuMenuCardCommand, int>
    {
        private readonly IUnitOfWork repository;

        public DeleteMenuMenuCardCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteMenuMenuCardCommand request, CancellationToken cancellationToken)
        {
            var menuCard = repository.MenuCards.GetMenuCardIncludingDependencies(request.Model.MenuCardId);
            var menu = repository.Menus.Get(request.Model.MenuId);
            menuCard.Menus.Remove(menu);
            repository.Menus.Delete(menu.Id);
            await repository.CommitAsync();
            return request.Model.MenuId;
        }
    }
}