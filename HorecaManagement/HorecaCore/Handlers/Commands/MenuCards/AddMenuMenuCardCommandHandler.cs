using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.MenuCards
{
    public class AddMenuMenuCardCommand : IRequest<int>
    {
        public AddMenuMenuCardCommand(MutateMenuMenuCardDto model)
        {
            Model = model;
        }

        public MutateMenuMenuCardDto Model { get; }
    }

    public class AddMenuMenuCardCommandHandler : IRequestHandler<AddMenuMenuCardCommand, int>
    {
        private readonly IUnitOfWork repository;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public AddMenuMenuCardCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(AddMenuMenuCardCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to add {@object} to menucard with Id: {Id}", request.Model.Menu, request.Model.MenuCardId);

            var menuCard = await repository.MenuCards.GetMenuCardIncludingMenus(request.Model.MenuCardId, request.Model.RestaurantId);

            var entity = new Menu
            {
                Name = request.Model.Menu.Name,
                Description = request.Model.Menu.Description,
                Category = request.Model.Menu.Category,
                Price = request.Model.Menu.Price,
            };

            menuCard.Menus.Add(entity);
            repository.MenuCards.Update(menuCard);
            await repository.CommitAsync();

            // now when the entity exists in the db - attach the restaurant as FK
            entity.Restaurant = menuCard.Restaurant;
            repository.Menus.Update(entity);
            await repository.CommitAsync();
            logger.Info("succes adding {@object} to menucard with id {id}", entity, menuCard.Id);

            return entity.Id;
        }
    }
}