using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.MenuCards
{
    public class EditMenuCardCommand : IRequest<int>
    {
        public EditMenuCardCommand(MutateMenuCardDto model, int id, int restaurantId)
        {
            Model = model;
            Id = id;
            RestaurantId = restaurantId;
        }

        public MutateMenuCardDto Model { get; }
        public int Id { get; }
        public int RestaurantId { get; }
    }

    public class EditMenuCardCommandHandler : IRequestHandler<EditMenuCardCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public EditMenuCardCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditMenuCardCommand request, CancellationToken cancellationToken)
        {
            ValidateModelIds(request);
            logger.Info("trying to edit {object} with Id: {Id}", request.Model, request.Model.Id);
            var menuCard = await repository.MenuCards.GetMenuCardById(request.Model.Id, request.Model.RestaurantId);

            if (menuCard is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }

            menuCard.Name = request.Model.Name ?? menuCard.Name;

            repository.MenuCards.Update(menuCard);
            await repository.CommitAsync();
            logger.Info("updated {@object} with Id: {id}", menuCard, menuCard.Id);

            return menuCard.Id;
        }

        private static void ValidateModelIds(EditMenuCardCommand request)
        {
            if (request.Model.Id == 0)
            {
                request.Model.Id = request.Id;
            }
            if (request.Model.RestaurantId == 0)
            {
                request.Model.RestaurantId = request.RestaurantId;
            }
        }
    }
}