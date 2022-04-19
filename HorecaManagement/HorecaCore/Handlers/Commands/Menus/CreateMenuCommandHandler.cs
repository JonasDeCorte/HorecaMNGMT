using FluentValidation;
using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using Horeca.Shared.Dtos.Menus;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Menus
{
    public class CreateMenuCommand : IRequest<int>
    {
        public MutateMenuDto Model { get; }

        public CreateMenuCommand(MutateMenuDto model)
        {
            Model = model;
        }
    }

    public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, int>

    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public CreateMenuCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to create {object} with Id: {Id}", nameof(Menu), request.Model.Id);
            var restaurant = repository.Restaurants.Get(request.Model.RestaurantId);

            if (restaurant == null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }
            var entity = new Menu
            {
                Name = request.Model.Name,
                Description = request.Model.Description,
                Category = request.Model.Category,
                Price = request.Model.Price
            };

            repository.Menus.Add(entity);

            await repository.CommitAsync();

            // now when the entity exists in the db - attach the restaurant as FK
            entity.Restaurant = restaurant;
            repository.Menus.Update(entity);
            await repository.CommitAsync();
            logger.Info("adding {@object} with id {id}", entity, entity.Id);

            return entity.Id;
        }
    }
}