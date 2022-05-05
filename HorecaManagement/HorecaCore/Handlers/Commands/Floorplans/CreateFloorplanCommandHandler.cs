using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Floorplans;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Floorplans
{
    public class CreateFloorplanCommand : IRequest<int>
    {
        public MutateFloorplanDto Model { get; }
        public int RestaurantId { get; }

        public CreateFloorplanCommand(MutateFloorplanDto model, int restaurantId)
        {
            Model = model;
            RestaurantId = restaurantId;
        }
    }

    public class CreateFloorplanCommandHandler : IRequestHandler<CreateFloorplanCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public CreateFloorplanCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(CreateFloorplanCommand request, CancellationToken cancellationToken)
        {
            ValidateModelIds(request);
            logger.Info("trying to create {object}", nameof(Floorplan));
            var restaurant = repository.Restaurants.Get(request.Model.Restaurant.Id);

            if (restaurant == null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }

            var entity = new Floorplan
            {
                Name = request.Model.Name,
            };

            repository.Floorplans.Add(entity);
            await repository.CommitAsync();

            // now when the entity exists in the db - attach the restaurant as FK
            entity.Restaurant = restaurant;
            repository.Floorplans.Update(entity);
            await repository.CommitAsync();
            logger.Info("adding {@object} with id {id}", entity, entity.Id);

            return entity.Id;
        }

        private static void ValidateModelIds(CreateFloorplanCommand request)
        {
            if (request.Model.Restaurant.Id == 0)
            {
                request.Model.Restaurant.Id = request.RestaurantId;
            }
        }
    }
}
