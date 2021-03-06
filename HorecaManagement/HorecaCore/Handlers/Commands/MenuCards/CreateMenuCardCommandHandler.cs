using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.MenuCards;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.MenuCards
{
    public class CreateMenuCardCommand : IRequest<int>
    {
        public CreateMenuCardCommand(MutateMenuCardDto model, int restaurantId)
        {
            Model = model;
            RestaurantId = restaurantId;
        }

        public MutateMenuCardDto Model { get; }
        public int RestaurantId { get; }
    }

    public class CreateMenuCardCommandHandler : IRequestHandler<CreateMenuCardCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public CreateMenuCardCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(CreateMenuCardCommand request, CancellationToken cancellationToken)
        {
            ValidateModelIds(request);
            logger.Info("trying to create {object}", nameof(MenuCard));

            var restaurant = repository.Restaurants.Get(request.Model.RestaurantId);

            if (restaurant == null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }
            var entity = new MenuCard
            {
                Name = request.Model.Name,
            };

            repository.MenuCards.Add(entity);
            await repository.CommitAsync();

            // now when the entity exists in the db - attach the restaurant as FK
            entity.Restaurant = restaurant;
            repository.MenuCards.Update(entity);
            await repository.CommitAsync();
            logger.Info("adding {@object} with id {id}", entity, entity.Id);

            return entity.Id;
        }

        private static void ValidateModelIds(CreateMenuCardCommand request)
        {
            if (request.Model.RestaurantId == 0)
            {
                request.Model.RestaurantId = request.RestaurantId;
            }
        }
    }
}