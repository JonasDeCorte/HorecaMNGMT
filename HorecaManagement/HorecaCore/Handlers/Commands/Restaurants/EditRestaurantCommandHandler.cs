using Horeca.Core.Exceptions;
using Horeca.Shared.Data;
using Horeca.Shared.Dtos.Restaurants;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Restaurants
{
    public class EditRestaurantCommand : IRequest<int>
    {
        public EditRestaurantDto Model { get; }

        public EditRestaurantCommand(EditRestaurantDto model)
        {
            Model = model;
        }
    }

    public class EditRestaurantCommandHandler : IRequestHandler<EditRestaurantCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public EditRestaurantCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(EditRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to edit {@object} with Id: {Id}", request.Model, request.Model.Id);

            var Restaurant = repository.Restaurants.Get(request.Model.Id);

            if (Restaurant is null)
            {
                logger.Error(EntityNotFoundException.Instance);

                throw new EntityNotFoundException();
            }

            Restaurant.Name = request.Model.Name ?? Restaurant.Name;

            repository.Restaurants.Update(Restaurant);

            await repository.CommitAsync();
            logger.Info("updated {@object} with Id: {id}", Restaurant, Restaurant.Id);

            return Restaurant.Id;
        }
    }
}