using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using MediatR;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeca.Core.Handlers.Commands.Restaurants
{
    public class DeleteMenuCardFromRestaurantCommand : IRequest<int>
    {
        public DeleteMenuCardFromRestaurantCommand(int restaurantId, int menuCardId)
        {
            RestaurantId = restaurantId;
            MenuCardId = menuCardId;
        }

        public int RestaurantId { get; }
        public int MenuCardId { get; }
    }

    public class DeleteMenuCardFromRestaurantCommandHandler : IRequestHandler<DeleteMenuCardFromRestaurantCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteMenuCardFromRestaurantCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteMenuCardFromRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to get {object} with id: {id}", nameof(Restaurants), request.RestaurantId);

            var restaurant = await repository.Restaurants.GetRestaurantIncludingMenuCardsById(request.RestaurantId);
            if (restaurant == null)
            {
                logger.Error(EntityNotFoundException.Instance);
                throw new EntityNotFoundException();
            }
            logger.Info("trying to get {object} with id: {id}", nameof(MenuCard), request.MenuCardId);

            var menuCard = repository.MenuCards.Get(request.MenuCardId);

            if (menuCard == null)
            {
                logger.Error(EntityNotFoundException.Instance);
                throw new EntityNotFoundException();
            }
            logger.Info("removing menucard with id: {id}", request.MenuCardId);
            restaurant.MenuCards.Remove(menuCard);
            repository.Restaurants.Update(restaurant);
            repository.MenuCards.Delete(menuCard.Id);
            await repository.CommitAsync();

            return restaurant.Id;
        }
    }
}