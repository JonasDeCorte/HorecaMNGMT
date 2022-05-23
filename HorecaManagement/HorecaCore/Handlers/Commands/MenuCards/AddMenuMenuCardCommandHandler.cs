using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.MenuCards
{
    public class AddMenuMenuCardCommand : IRequest<int>
    {
        public AddMenuMenuCardCommand(MutateMenuMenuCardDto model, int id, int restaurantId)
        {
            Model = model;
            Id = id;
            RestaurantId = restaurantId;
        }

        public MutateMenuMenuCardDto Model { get; }
        public int Id { get; }
        public int RestaurantId { get; }
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
            ValidateModelIds(request);
            logger.Info("trying to add {@object} to menucard with Id: {Id}", request.Model.Menu, request.Model.MenuCardId);

            var menuCard = await repository.MenuCards.GetMenuCardIncludingMenus(request.Model.MenuCardId, request.Model.RestaurantId);
            Menu entity;
            if (request.Model.Menu.Id == 0)
            {
                entity = new Menu
                {
                    Name = request.Model.Menu.Name,
                    Description = request.Model.Menu.Description,
                    Category = request.Model.Menu.Category,
                    Price = request.Model.Menu.Price,
                };
            }
            else
            {
                logger.Info("menu exists, get Menu from database  {id} ", request.Model.Menu.Id);
                entity = await repository.Menus.GetMenuById(request.Model.Menu.Id, request.Model.Menu.RestaurantId);
                if (entity == null)
                {
                    logger.Error(EntityNotFoundException.Instance);
                    throw new EntityNotFoundException();
                }
                logger.Info("check if menuCard contains dish with id {id}", entity.Id);
                var existingMenu = menuCard.Menus.SingleOrDefault(x => x.Id.Equals(entity.Id));

                if (existingMenu != null)
                {
                    logger.Error(EntityIsAlreadyPartOfThisCollectionException.Instance);
                    throw new EntityIsAlreadyPartOfThisCollectionException();
                }
            }

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

        private static void ValidateModelIds(AddMenuMenuCardCommand request)
        {
            if (request.Model.MenuCardId == 0)
            {
                request.Model.MenuCardId = request.Id;
            }
            if (request.Model.RestaurantId == 0)
            {
                request.Model.RestaurantId = request.RestaurantId;
            }
        }
    }
}