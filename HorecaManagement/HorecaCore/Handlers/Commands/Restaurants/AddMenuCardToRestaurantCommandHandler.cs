using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Restaurants;
using MediatR;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeca.Core.Handlers.Commands.Restaurants
{
    public class AddMenuCardToRestaurantCommand : IRequest<int>
    {
        public AddMenuCardToRestaurantCommand(int restaurantId, int menuCardId)
        {
            RestaurantId = restaurantId;
            MenuCardId = menuCardId;
        }

        public int RestaurantId { get; }
        public int MenuCardId { get; }
    }

    public class AddMenuCardToRestaurantCommandHandler : IRequestHandler<AddMenuCardToRestaurantCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public AddMenuCardToRestaurantCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(AddMenuCardToRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to get {object} with id: {id}", nameof(Restaurant), request.RestaurantId);

            var restaurant = await repository.Restaurants.GetRestaurantIncludingMenuCardsById(request.RestaurantId);
            if (restaurant == null)
            {
                logger.Error(EntityNotFoundException.Instance);
                throw new EntityNotFoundException();
            }
            var menuCard = repository.MenuCards.Get(request.MenuCardId);

            if (menuCard == null)
            {
                logger.Error(EntityNotFoundException.Instance);
                throw new EntityNotFoundException();
            }
            logger.Info("trying to add {object} with id: {id} to {resto} with id: {restId}", nameof(MenuCard), menuCard.Id, nameof(Restaurant), restaurant.Id);

            restaurant.MenuCards.Add(menuCard);
            repository.Restaurants.Update(restaurant);
            repository.MenuCards.Update(menuCard);
            await repository.CommitAsync();
            return restaurant.Id;
        }
    }
}