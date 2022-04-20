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

        public CreateUnitCommand(MutateUnitDto model)
        {
            Model = model;
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
                logger.Info("trying to create {@object} with Id: {Id}", nameof(Shared.Data.Entities.Unit), request.Model.Id);

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
        }
    }
}