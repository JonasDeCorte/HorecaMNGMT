using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Units;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Units
{
    public class CreateUnitCommand : IRequest<int>
    {
        public MutateUnitDto Model { get; }
        public int RestaurantId { get; }

        public CreateUnitCommand(MutateUnitDto model, int restaurantId)
        {
            Model = model;
            RestaurantId = restaurantId;
        }

        public class CreateUnitCommandHandler : IRequestHandler<CreateUnitCommand, int>
        {
            private readonly IUnitOfWork repository;
            private static readonly Logger logger = LogManager.GetCurrentClassLogger();

            public CreateUnitCommandHandler(IUnitOfWork repository)
            {
                this.repository = repository;
            }

            public async Task<int> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
            {
                ValidateModelIds(request);
                logger.Info("trying to create {@object}", nameof(Shared.Data.Entities.Unit));
                var restaurant = repository.Restaurants.Get(request.Model.RestaurantId);

                if (restaurant == null)
                {
                    logger.Error(EntityNotFoundException.Instance);

                    throw new EntityNotFoundException();
                }
                var entity = new Shared.Data.Entities.Unit
                {
                    Name = request.Model.Name,
                };
                repository.Units.Add(entity);
                await repository.CommitAsync();
                // now when the entity exists in the db - attach the restaurant as FK
                entity.Restaurant = restaurant;
                repository.Units.Update(entity);
                await repository.CommitAsync();
                logger.Info("adding {@unit} with id {id}", entity, entity.Id);
                return entity.Id;
            }

            private static void ValidateModelIds(CreateUnitCommand request)
            {
                if (request.Model.RestaurantId == 0)
                {
                    request.Model.RestaurantId = request.RestaurantId;
                }
            }
        }
    }
}