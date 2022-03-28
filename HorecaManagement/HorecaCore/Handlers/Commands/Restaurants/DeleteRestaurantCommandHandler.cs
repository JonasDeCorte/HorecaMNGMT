using Horeca.Shared.Data;
using Horeca.Shared.Data.Entities;
using MediatR;
using NLog;

namespace Horeca.Core.Handlers.Commands.Restaurants
{
    public class DeleteRestaurantCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteRestaurantCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand, int>
    {
        private readonly IUnitOfWork repository;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public DeleteRestaurantCommandHandler(IUnitOfWork repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.Info("trying to delete {object} with Id: {id}", nameof(Restaurant), request.Id);

            repository.Restaurants.Delete(request.Id);

            await repository.CommitAsync();

            logger.Info("deleted {object} with Id: {id}", nameof(Restaurant), request.Id);

            return request.Id;
        }
    }
}