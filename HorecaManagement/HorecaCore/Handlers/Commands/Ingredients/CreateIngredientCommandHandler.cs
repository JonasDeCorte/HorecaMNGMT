using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Ingredients;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Ingredients
{
    public class CreateIngredientCommand : IRequest<int>
    {
        public MutateIngredientDto Model { get; }

        public CreateIngredientCommand(MutateIngredientDto model)
        {
            Model = model;
        }
    }

    public class CreateIngredientCommandHandler : IRequestHandler<CreateIngredientCommand, int>

    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public CreateIngredientCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to create {@object} with Id: {Id}", nameof(Ingredient), request.Model.Id);
            var restaurant = repository.Restaurants.Get(request.Model.RestaurantId);

            if (restaurant == null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }
            var entity = new Ingredient
            {
                Name = request.Model.Name,
                BaseAmount = request.Model.BaseAmount,
                IngredientType = request.Model.IngredientType,
                Unit = new Shared.Data.Entities.Unit
                {
                    Name = request.Model.Unit.Name,
                },
            };
            repository.Ingredients.Add(entity);
            await repository.CommitAsync();

            // now when the entity exists in the db - attach the restaurant as FK
            entity.Restaurant = restaurant;
            repository.Ingredients.Update(entity);
            await repository.CommitAsync();

            logger.Info("adding {@object} with id {id}", entity, entity.Id);

            return entity.Id;
        }
    }
}