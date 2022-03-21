using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.MenuCards
{
    public class EditMenuCardCommand : IRequest<int>
    {
        public EditMenuCardCommand(MutateMenuCardDto model)
        {
            Model = model;
        }

        public MutateMenuCardDto Model { get; }
    }

    public class EditMenuCardCommandHandler : IRequestHandler<EditMenuCardCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public EditMenuCardCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditMenuCardCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to edit {object} with Id: {Id}", request.Model, request.Model.Id);
            var menuCard = repository.MenuCards.Get(request.Model.Id);

            if (menuCard is null)
            {
                logger.Error("{Object} with Id: {id} does not exist", nameof(menuCard), request.Model.Id);

                throw new EntityNotFoundException("Entity does not exist");
            }

            menuCard.Name = request.Model.Name ?? menuCard.Name;

            repository.MenuCards.Update(menuCard);
            await repository.CommitAsync();
            logger.Info("updated {@object} with Id: {id}", menuCard, menuCard.Id);

            return menuCard.Id;
        }
    }
}