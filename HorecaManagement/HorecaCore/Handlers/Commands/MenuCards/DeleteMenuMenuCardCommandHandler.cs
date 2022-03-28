using Horeca.Shared.Data;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;
using NLog;

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
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteMenuMenuCardCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteMenuMenuCardCommand request, CancellationToken cancellationToken)
        {
            var menuCard = repository.MenuCards.GetMenuCardIncludingDependencies(request.Model.MenuCardId);
            var menu = repository.Menus.Get(request.Model.MenuId);
            logger.Info("trying to delete {@object} with id {objId} from {@dish} with Id: {id}", menu, request.Model.MenuId, menuCard, request.Model.MenuCardId);

            menuCard.Menus.Remove(menu);
            repository.Menus.Delete(menu.Id);

            await repository.CommitAsync();

            logger.Info("deleted {@object} with id {objId} from {@dish} with Id: {id}", menu, request.Model.MenuId, menuCard, request.Model.MenuCardId);

            return request.Model.MenuId;
        }
    }
}